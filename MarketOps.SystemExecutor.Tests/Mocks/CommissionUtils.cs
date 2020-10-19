using MarketOps.SystemExecutor.Interfaces;
using NSubstitute;

namespace MarketOps.SystemExecutor.Tests.Mocks
{
    /// <summary>
    /// Utils for ICommission mocks.
    /// </summary>
    internal static class CommissionUtils
    {
        public static ICommission CreateSubstitute()
        {
            return CreateSubstitute(0);
        }

        public static ICommission CreateSubstitute(float returnedCommission)
        {
            ICommission commission = Substitute.For<ICommission>();
            commission.Calculate(default, default, default).ReturnsForAnyArgs(returnedCommission);
            return commission;
        }
    }
}
