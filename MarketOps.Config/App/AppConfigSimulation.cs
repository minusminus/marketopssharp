using System;

namespace MarketOps.Config.App
{
    /// <summary>
    /// Application configuration for simulation tab.
    /// </summary>
    public class AppConfigSimulation
    {
        public string SystemChoice { get; set; }
        public DateTime SimFrom { get; set; }
        public DateTime SimTo { get; set; }
        public decimal InitialCash { get; set; }
        public decimal MonteCarloCount { get; set; }
        public decimal MonteCarloLength { get; set; }
    }
}
