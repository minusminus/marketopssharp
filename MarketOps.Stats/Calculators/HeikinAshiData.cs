using System;

namespace MarketOps.Stats.Calculators
{
    /// <summary>
    /// Heikin-Ashi result data
    /// </summary>
    public class HeikinAshiData
    {
        public readonly float[] O;
        public readonly float[] H;
        public readonly float[] L;
        public readonly float[] C;
        public readonly DateTime[] TS;

        public HeikinAshiData(int length)
        {
            O = new float[length];
            H = new float[length];
            L = new float[length];
            C = new float[length];
            TS = new DateTime[length];
        }
    }
}
