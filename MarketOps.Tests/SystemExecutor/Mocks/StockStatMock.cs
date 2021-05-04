using MarketOps.StockData.Types;

namespace MarketOps.Tests.SystemExecutor.Mocks
{
    /// <summary>
    /// Stock stat mock.
    /// </summary>
    public class StockStatMock : StockStat
    {
        public StockStatMock(string chartArea, int returnedBackBufferLength) : base(chartArea)
        {
            ReturnedBackBufferLength = returnedBackBufferLength;
            CalculateCallCount = 0;
        }

        public int ReturnedBackBufferLength;
        public int CalculateCallCount;

        protected override void InitializeData()
        {
            _name = "StatMock";
        }

        protected override void InitializeStatParams()
        {
        }

        protected override int GetBackBufferLength()
        {
            return ReturnedBackBufferLength;
        }

        public override void Calculate(StockPricesData data)
        {
            CalculateCallCount++;
        }
    }
}
