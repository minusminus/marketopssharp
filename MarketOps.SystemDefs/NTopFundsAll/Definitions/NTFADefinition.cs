namespace MarketOps.SystemDefs.NTopFundsAll.Definitions
{
    /// <summary>
    /// RiskMultiplier - Mnożnik wyliczonego ryzyka do balansu. 
    ///     Wartość > 1 zwiększa ryzyko w balansie zmniejszając wielkośc danej pozycji.
    ///     Wartość < 1 zmniejsza ryzyko w balansie zwiększając wielkośc danej pozycji.
    /// </summary>
    internal class NTFASingleDefinition
    {
        public string Name;
        public double RiskMultiplier;
    }

    /// <summary>
    /// N top funds all processed funds definition.
    /// </summary>
    internal abstract class NTFADefinition
    {
        protected NTFASingleDefinition _passiveStock;
        protected NTFASingleDefinition[] _balancedStocks;

        public NTFASingleDefinition PassiveStock => _passiveStock;

        public NTFASingleDefinition[] BalancedStocks => _balancedStocks; 

        public int Length => _balancedStocks.Length;

        public NTFASingleDefinition this[int i] => _balancedStocks[i];
    }
}
