namespace MarketOps.SystemDefs.NTopFundsAll.Definitions
{
    /// <summary>
    /// PKO funds distributed by MBank.
    /// </summary>
    internal class NTFAMbankPko : NTFADefinition
    {
        public NTFAMbankPko()
        {
            _passiveStock = new NTFASingleDefinition() { Name = "PKO014" };

            _balancedStocks = new[]{
                new NTFASingleDefinition(){Name = "PKO002", RiskMultiplier = 1},    //stab wzrostu
                new NTFASingleDefinition(){Name = "PKO005", RiskMultiplier = 1},    //obl skarbowych
                new NTFASingleDefinition(){Name = "PKO014", RiskMultiplier = 1},    //obl dlugoterm
                new NTFASingleDefinition(){Name = "PKO009", RiskMultiplier = 1},    //wylaczony zlota 
                new NTFASingleDefinition(){Name = "PKO010", RiskMultiplier = 1},    //wylaczony pap dluznych usd 
                new NTFASingleDefinition(){Name = "PKO013", RiskMultiplier = 1},    //wylaczony akcji rynku azji i pacyfiku
                new NTFASingleDefinition(){Name = "PKO015", RiskMultiplier = 1},  //akcji mis
                new NTFASingleDefinition(){Name = "PKO018", RiskMultiplier = 1},    //wylaczony akcji rynku amerykanskiego
                new NTFASingleDefinition(){Name = "PKO019", RiskMultiplier = 1},    //wylaczony akcji rynku japonskiego
                new NTFASingleDefinition(){Name = "PKO021", RiskMultiplier = 1},  //akcji plus
                new NTFASingleDefinition(){Name = "PKO025", RiskMultiplier = 1},    //wylaczony akcji rynkow wschodzacych
                new NTFASingleDefinition(){Name = "PKO026", RiskMultiplier = 1},  //surowcow globalny
                new NTFASingleDefinition(){Name = "PKO027", RiskMultiplier = 1},  //tech i innowacji globalny
                new NTFASingleDefinition(){Name = "PKO028", RiskMultiplier = 1},  //dobr luksusowych globalny
                new NTFASingleDefinition(){Name = "PKO029", RiskMultiplier = 1},  //infrastruktury i bud globalny
                new NTFASingleDefinition(){Name = "PKO057", RiskMultiplier = 1},  //medycyny i demografii globalny
                //new NTFASingleDefinition(){Name = "PKO072", RiskMultiplier = 1},    //brak ekologii i odp spolecznej globalny
                //new NTFASingleDefinition(){Name = "PKO073", RiskMultiplier = 1},    //brak obl rynku polskiego
                //new NTFASingleDefinition(){Name = "PKO074", RiskMultiplier = 1},    //brak obligacji globalny
                //new NTFASingleDefinition(){Name = "PKO097", RiskMultiplier = 1},    //brak akcji dywidendowych globalny
                new NTFASingleDefinition(){Name = "PKO098", RiskMultiplier = 1},  //akcji rynku europejskiego
                new NTFASingleDefinition(){Name = "PKO909", RiskMultiplier = 1},  //akcji rynku zlota
                new NTFASingleDefinition(){Name = "PKO910", RiskMultiplier = 1},  //pap dluznych usd
                new NTFASingleDefinition(){Name = "PKO913", RiskMultiplier = 1},  //akcji rynku polskiego
                new NTFASingleDefinition(){Name = "PKO918", RiskMultiplier = 1},  //akcji rynku amerykanskiego
                new NTFASingleDefinition(){Name = "PKO919", RiskMultiplier = 1},  //akcji rynku japonskiego
                new NTFASingleDefinition(){Name = "PKO925", RiskMultiplier = 1}   //akcji rynkow wschodzacych
            };
        }
    }
}
