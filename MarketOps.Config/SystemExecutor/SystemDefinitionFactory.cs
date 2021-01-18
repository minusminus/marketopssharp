using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Linq;

namespace MarketOps.Config.SystemExecutor
{
    /// <summary>
    /// Factory of SystemDefinition objects.
    /// </summary>
    public class SystemDefinitionFactory
    {
        private readonly IStockDataProvider _dataProvider;
        private readonly ISystemDataLoader _dataLoader;
        private readonly ISlippage _slippage;
        private readonly ICommission _commission;

        public SystemDefinitionFactory(IStockDataProvider dataProvider, ISystemDataLoader dataLoader, ISlippage slippage, ICommission commission)
        {
            _dataProvider = dataProvider;
            _dataLoader = dataLoader;
            _slippage = slippage;
            _commission = commission;
        }

        public SystemDefinition Get(ConfigSystemDefinition config)
        {
            SystemDefinition res = CreateObject(ClassesLoader.FindType(config.ClassLibrary, config.ClassName));
            SetDefaultParams(res, config.ParamsDefaults);
            return res;
        }

        private SystemDefinition CreateObject(Type type)
        {
            if (!ExpectedConstructorExists(type))
                throw new Exception($"Suitable constructor not found in {type.FullName}");

            return (SystemDefinition)Activator.CreateInstance(type, CreateConstructorParams());
        }

        private bool ExpectedConstructorExists(Type type) =>
            type.GetConstructors().Any(c => c.GetParameters().Length == 4);

        private object[] CreateConstructorParams() =>
            new object[] {
                _dataProvider,
                _dataLoader,
                _slippage,
                _commission
            };

        private void SetDefaultParams(SystemDefinition systemDefinition, ConfigSystemDefinitionParamDefault[] paramsDefaults)
        {
            foreach (var paramDef in paramsDefaults)
                systemDefinition.SystemParams.Get(paramDef.Param).ValueString = paramDef.Value;
        }
    }
}
