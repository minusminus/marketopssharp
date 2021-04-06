using System;

namespace MarketOps.SystemDefs
{
    /// <summary>
    /// Counts mod n and signals on (mod n == 0).
    /// </summary>
    internal class ModNCounter
    {
        private readonly int _n;
        private int _counter;

        public ModNCounter(int n)
        {
            if (n < 1)
                throw new ArgumentOutOfRangeException("ModNCounter.n", "Value should be greater than 0.");
            _n = n;
            Reset();
        }

        public void Reset() => _counter = 0;

        public void Next() => _counter++;

        public bool IsZero => _counter % _n == 0;
    }
}
