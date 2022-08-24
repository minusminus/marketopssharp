using System;
using NUnit.Framework;
using Shouldly;
using MarketOps.SystemAnalysis.SystemSummary;
using System.Linq;
using MarketOps.SystemData.Types;

namespace MarketOps.Tests.SystemAnalysis.SystemSummary
{
    [TestFixture]
    public class WinsLossesCalculatorTests
    {
        private SystemState _state;
        private SystemStateSummary _summary;

        [SetUp]
        public void SetUp()
        {
            _state = new SystemState();
            _summary = new SystemStateSummary();
        }

        [Test]
        public void Calculate_EmptyState__EmptySummary()
        {
            WinsLossesCalculator.Calculate(_summary, _state);

            _summary.StartTS.ShouldBe(DateTime.MinValue);
            _summary.StopTS.ShouldBe(DateTime.MinValue);
            _summary.ProcessedTicks.ShouldBe(0);
            _summary.InitialValue.ShouldBe(0);
            _summary.FinalValueOnLastTick.ShouldBe(0);
            _summary.FinalValueOnClosedPositions.ShouldBe(0);
            _summary.ClosedPositionsCount.ShouldBe(0);
            _summary.Wins.ShouldBe(0);
            _summary.Losses.ShouldBe(0);
            _summary.WinProbability.ShouldBe(0);
            _summary.LossProbability.ShouldBe(0);
            _summary.SumWins.ShouldBe(0);
            _summary.SumLosses.ShouldBe(0);
            _summary.AvgWin.ShouldBe(0);
            _summary.AvgPcntWin.ShouldBe(0);
            _summary.AvgLoss.ShouldBe(0);
            _summary.AvgPcntLoss.ShouldBe(0);
            _summary.AvgWinLossRatio.ShouldBe(0);
            _summary.ExpectedPositionValue.ShouldBe(0);
        }

        [Test]
        public void Calculate_NonEmptyState__CorrectCummary()
        {
            _state.PositionsClosed.AddRange(Enumerable.Range(1, 3).Select(_ => new Position()
            {
                Direction = PositionDir.Long,
                Open = 10,
                EquityValueOnTickBeforeOpen = 1000,
                Close = 9,
                Volume = 1,
            }));
            _state.PositionsClosed.Add(new Position()
            {
                Direction = PositionDir.Long,
                Open = 10,
                EquityValueOnTickBeforeOpen = 1000,
                Close = 20,
                Volume = 1,
            });
            _state.Equity.AddRange(Enumerable.Range(1, 10).Select(i => new SystemValue()
            {
                TS = DateTime.Now.AddDays(i),
                Value = i
            }));

            WinsLossesCalculator.Calculate(_summary, _state);

            _summary.StartTS.ShouldBe(DateTime.MinValue);
            _summary.StopTS.ShouldBe(DateTime.MinValue);
            _summary.ProcessedTicks.ShouldBe(10);
            _summary.InitialValue.ShouldBe(0);
            _summary.FinalValueOnLastTick.ShouldBe(0);
            _summary.FinalValueOnClosedPositions.ShouldBe(0);
            _summary.ClosedPositionsCount.ShouldBe(4);
            _summary.Wins.ShouldBe(1);
            _summary.Losses.ShouldBe(3);
            _summary.WinProbability.ShouldBe(0.25f);
            _summary.LossProbability.ShouldBe(0.75f);
            _summary.SumWins.ShouldBe(10);
            _summary.SumLosses.ShouldBe(3);
            _summary.AvgWin.ShouldBe(10f);
            _summary.AvgPcntWin.ShouldBe(0.01f);
            _summary.AvgLoss.ShouldBe(1f);
            _summary.AvgPcntLoss.ShouldBe(-0.001f);
            _summary.AvgWinLossRatio.ShouldBe(10f);
            _summary.ExpectedPositionValue.ShouldBe(1.75f);
        }
    }
}
