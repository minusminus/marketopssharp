using System;
using System.Collections.Generic;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameters container for calculating stock data statistics
    /// </summary>
    public class StockStatParams
    {
        private readonly Dictionary<string, StockStatParam> _params = new Dictionary<string, StockStatParam>();

        public void Set(string paramName, StockStatParam paramValue)
        {
            _params[paramName] = paramValue;
        }

        public StockStatParam Get(string paramName)
        {
            if (!_params.TryGetValue(paramName, out StockStatParam value))
                throw new Exception($"Undefined parameter {paramName}");
            return value;
        }

        public IEnumerator<StockStatParam> GetEnumerator() => (IEnumerator<StockStatParam>) _params.Values.GetEnumerator();
    }
}
