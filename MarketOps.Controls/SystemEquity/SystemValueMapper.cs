using MarketOps.SystemExecutor;
using System;

namespace MarketOps.Controls.SystemEquity
{
    /// <summary>
    /// Maps SystemValue.
    /// </summary>
    internal class SystemValueMapper
    {
        private readonly SystemValue _value;

        public SystemValueMapper(SystemValue value)
        {
            _value = value;
        }

        public float Value => _value.Value;
        public DateTime TS => _value.TS;
    }
}
