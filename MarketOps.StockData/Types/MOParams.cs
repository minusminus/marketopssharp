using System;
using System.Collections.Generic;

namespace MarketOps.StockData.Types
{
    /// <summary>
    /// Parameters container.
    /// </summary>
    public class MOParams
    {
        private readonly Dictionary<string, MOParam> _params = new Dictionary<string, MOParam>();

        public void Set(string paramName, MOParam paramValue)
        {
            _params[paramName] = paramValue;
        }

        public MOParam Get(string paramName)
        {
            if (!_params.TryGetValue(paramName, out MOParam value))
                throw new Exception($"Undefined parameter {paramName}");
            return value;
        }

        public IEnumerator<MOParam> GetEnumerator() => (IEnumerator<MOParam>) _params.Values.GetEnumerator();
    }
}
