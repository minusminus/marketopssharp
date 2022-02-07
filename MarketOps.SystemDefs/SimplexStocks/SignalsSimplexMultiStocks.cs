using MarketOps.StockData.Extensions;
using MarketOps.StockData.Interfaces;
using MarketOps.StockData.Types;
using MarketOps.SystemData.Extensions;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketOps.SystemDefs.SimplexStocks
{
    /// <summary>
    /// Signals for multi stocks with simplex balance calculation.
    /// 
    /// First fund in list is safe fund.
    /// 
    /// --== Original version ==--
    /// acceptable_risk = 0.1
    /// sigma_multiplier = 2
    /// max_single_pos_size = 0.8
    /// 
    /// calculations:
    /// average change close - close of 3 and 6 periods
    /// stddev for avgchange(6)
    /// 
    /// simplex solver:
    /// goal: max avg 3 periods profit
    /// constaints for each decision:
    /// - decision >= 0
    /// - avgchange(6) + stddev * sigma_multiplier <= portoflio_value * acceptable_risk
    /// - price * decision <= portoflio_value * max_single_pos_size
    /// </summary>
    internal class SignalsSimplexMultiStocks : ISystemDataDefinitionProvider, ISignalGeneratorOnClose
    {
        private int _avgProfitRange;// = 3;
        private int _avgChangeRange;// = 6;
        private double _acceptableSingleDD;// = 0.1;
        private double _riskSigmaMultiplier;// = 2;
        private double _maxSinglePositionSize;// = 0.8;
        private double _maxPortfolioRisk;// = 0.8;
        private int _truncateBalanceToNthPlace;// = 3;    //balance in range <0..1> - truncate to first place after comma

        //spolki o obrocie >50000 z ost 3 miesiecy od 2021-09-01 do 12-01
        private readonly string[] _stocksNames = { "PKO014",
            "KGHM", "PKOBP", "PEKAO", "PZU", "PKNORLEN", "CDPROJEKT", "JSW", "MBANK", "LPP", "PGNIG", "ALIOR", "CYFRPLSAT", "SANPL", "PGE", "LOTOS", "CCC", "MERCATOR", 
            "ORANGEPL", "MABION", "BUMECH", "BOGDANKA", "TAURONPE", "MILLENNIUM", "KRUK", "GETINOBLE", "ASSECOPOL", "KERNEL", "CIECH", "KETY", "ASBIS", "GRUPAAZOTY", 
            "FAMUR", "ZEPAK", "XTB", "BIOMEDLUB", "EUROCASH", "HANDLOWY", "ENEA", "INGBSK", "GPW", "LIVECHAT", "INTERCARS", "11BIT", "BUDIMEX", "PKPCARGO", "AMREST", 
            "COALENERG", "WIRTUALNA", "COGNOR", "STALPROD", "CLNPHARMA", "BENEFIT", "CIGAMES", "POLIMEXMS", "SERINUS", "NEUCA", "TIM", "DOMDEV", "ASTARTA", "ACTION", 
            "WIELTON", "RAFAKO", "PLAYWAY", "BOS", "BOWIM", "AMICA", "GROCLIN", "MILKILAND", "GETIN", "CEZ", "AUTOPARTN", "ALUMETAL", "ASSECOSEE", "MIRBUD", "TOYA", 
            "SELVITA", "COMARCH", "GRODNO", "PRAIRIE", "OPONEO.PL", "KSGAGRO", "ARCTIC", "TRAKCJA", "SANOK", "FERRO"};

        private readonly bool[] _aggressiveFunds;

        private readonly ISystemDataLoader _dataLoader;
        private readonly ISystemExecutionLogger _systemExecutionLogger;
        private readonly StockDataRange _dataRange;
        private SimplexStocksData _fundsData;

        public SignalsSimplexMultiStocks(ISystemDataLoader dataLoader, IStockDataProvider dataProvider, ISystemExecutionLogger systemExecutionLogger, MOParams systemParams)
        {
            _avgProfitRange = systemParams.Get(SimplexMultiStocksParams.AvgProfitRange).As<int>();
            _avgChangeRange = systemParams.Get(SimplexMultiStocksParams.AvgChangeRange).As<int>();
            _acceptableSingleDD = systemParams.Get(SimplexMultiStocksParams.AcceptableSingleDD).As<double>();
            _riskSigmaMultiplier = systemParams.Get(SimplexMultiStocksParams.RiskSigmaMultiplier).As<double>();
            _maxSinglePositionSize = systemParams.Get(SimplexMultiStocksParams.MaxSinglePositionSize).As<double>();
            _maxPortfolioRisk = systemParams.Get(SimplexMultiStocksParams.MaxPortfolioRisk).As<double>();
            _truncateBalanceToNthPlace = systemParams.Get(SimplexMultiStocksParams.TruncateBalanceToNthPlace).As<int>();

            _aggressiveFunds = _stocksNames.Select((_, i) => i > 0).ToArray();
            if (_stocksNames.Length != _aggressiveFunds.Length)
                throw new Exception("_fundsNames != _aggressiveFunds");

            _dataLoader = dataLoader;
            _systemExecutionLogger = systemExecutionLogger;
            _dataRange = StockDataRange.Monthly;
            _fundsData = new SimplexStocksData(_stocksNames.Length);
            SimplexStocksDataCalculator.Initialize(_fundsData, _stocksNames, dataProvider);
        }

        public SystemDataDefinition GetDataDefinition() => new SystemDataDefinition()
        {
            stocks = _fundsData.Stocks
                .Select((def, i) =>
                {
                    return new SystemStockDataDefinition()
                    {
                        stock = def,
                        dataRange = _dataRange,
                        stats = new List<StockStat>() { }
                    };
                })
                .ToList()
        };

        public List<Signal> GenerateOnClose(DateTime ts, int leadingIndex, SystemState systemState)
        {
            List<Signal> result = new List<Signal>();

            SimplexStocksDataCalculator.Calculate(_fundsData, ts, _avgProfitRange, _avgChangeRange, _dataRange, _dataLoader);

            float portfolioValue = new SystemValueCalculator().Calc(systemState, ts, _dataLoader);
            float[] balance = SimplexExecutor.Execute(_fundsData,
                portfolioValue, _acceptableSingleDD, _riskSigmaMultiplier, _maxSinglePositionSize, _maxPortfolioRisk, _truncateBalanceToNthPlace);
            result.Add(CreateSignal(balance, _dataRange, _fundsData));

            LogData(ts, balance);
            return result;
        }

        private Signal CreateSignal(float[] newBalance, StockDataRange dataRange, SimplexStocksData fundsData) =>
            new Signal()
            {
                DataRange = dataRange,
                IntradayInterval = 0,
                Type = SignalType.EnterOnOpen,
                Direction = PositionDir.Long,
                Rebalance = true,
                NewBalance = fundsData.Stocks.Select((def, i) => (def, newBalance[i])).ToList()
            };


        private void LogData(DateTime ts, float[] balance)
        {
            _systemExecutionLogger.Add(
                $"{ts.Date:yyyy-MM-dd}:" + Environment.NewLine
                //+ string.Join(", ", _fundsNames.Select((name, i) => $"{name}[{_fundsData.Active[i]}, {100f * _fundsData.AvgProfit[i]:F2}, {100f * _fundsData.AvgChange[i]:F2}, {100f * _fundsData.AvgChangeSigma[i]:F2}]")) + Environment.NewLine
                //+ "balance: " + string.Join(", ", _fundsNames.Select((name, i) => $"{name}[{100f * balance[i]:F2}]")) + Environment.NewLine
                + "selected: " + string.Join(", ", _stocksNames.Select((name, i) => (name, i)).Where(x => balance[x.i] > 0).Select(x => $"{x.name}[{100f * balance[x.i]:F2}]")) + Environment.NewLine
                );
        }
    }
}
