using MarketOps.SystemData.Interfaces;
using NSubstitute;

namespace MarketOps.Tests.SystemExecutor.Mocks
{
    /// <summary>
    /// Utils for ISlippage mocks.
    /// </summary>
    public static class SlippageUtils
    {
        public static ISlippage CreateSusbstitute()
        {
            ISlippage slippage = Substitute.For<ISlippage>();
            slippage.CalculateClose(default, default, default, default).ReturnsForAnyArgs(x => x.ArgAt<float>(3));
            slippage.CalculateOpen(default, default, default, default).ReturnsForAnyArgs(x => x.ArgAt<float>(3));
            return slippage;
        }
    }
}
