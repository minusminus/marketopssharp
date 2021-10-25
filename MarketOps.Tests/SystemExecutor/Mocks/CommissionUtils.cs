using MarketOps.SystemData.Interfaces;
using NSubstitute;

namespace MarketOps.Tests.SystemExecutor.Mocks
{
    /// <summary>
    /// Utils for ICommission mocks.
    /// </summary>
    public static class CommissionUtils
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
