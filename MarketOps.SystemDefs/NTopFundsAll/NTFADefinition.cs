namespace MarketOps.SystemDefs.NTopFundsAll
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
    internal class NTFADefinition
    {
        public readonly NTFASingleDefinition PassiveStock = new NTFASingleDefinition() { Name = "PKO014" };

        public readonly NTFASingleDefinition[] BalancedStocks = {
            new NTFASingleDefinition(){Name = "PKO014", RiskMultiplier = 1},    //obl dlugoterm
            new NTFASingleDefinition(){Name = "PKO009", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO010", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO013", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO015", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO018", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO019", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO021", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO025", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO026", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO027", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO028", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO029", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO057", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO072", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO073", RiskMultiplier = 1},    //obl rynku polskiego
            new NTFASingleDefinition(){Name = "PKO074", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO097", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO098", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO909", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO910", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO913", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO918", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO919", RiskMultiplier = 1},
            new NTFASingleDefinition(){Name = "PKO925", RiskMultiplier = 1}
        };

        public int Length => BalancedStocks.Length;

        public NTFASingleDefinition this[int i] => BalancedStocks[i];
    }
}
