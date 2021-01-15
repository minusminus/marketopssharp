using MarketOps.StockData.Interfaces;
using MarketOps.SystemData.Interfaces;
using MarketOps.SystemData.Types;
using System;
using System.Linq;
using System.Reflection;

namespace MarketOps.SystemExecutor.ConfigSystemDefs
{
    /// <summary>
    /// Factory of SystemDefinition objects.
    /// </summary>
    internal class SystemDefinitionFactory
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
            Type systemDefType = FindType(config.ClassName, config.ClassLibrary);
            if (systemDefType == null)
                throw new Exception($"Class {config.ClassName} not found in {config.ClassLibrary}");

            return CreateObject(systemDefType);
        }

        private Type FindType(string className, string libraryName) =>
            Assembly.LoadFrom(libraryName).GetType(className);

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
    }
}
