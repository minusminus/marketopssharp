using MarketOps.SystemExecutor.Interfaces;
using NSubstitute;

namespace MarketOps.SystemExecutor.Tests.Mocks
{
    /// <summary>
    /// Utils for ISlippage mocks.
    /// </summary>
    internal static class SlippageUtils
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
