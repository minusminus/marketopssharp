namespace MarketOps.SystemDefs.NTopFundsAll.Definitions
{
    /// <summary>
    /// Original definition from first tests.
    /// </summary>
    internal class NTFAOriginalPko : NTFADefinition
    {
        public NTFAOriginalPko()
        {
            _passiveStock = new NTFASingleDefinition() { Name = "PKO014" };

            _balancedStocks = new[]{
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
        }
    }
}
