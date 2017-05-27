using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class ConfigurableFlatCaseStructure : IBlock
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXAttributeForClass]
        [YAXSerializeAs("PairedConfigurableMethodCall")]
        public string PairedConfigurableMethodCall { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableFlatCaseStructure.Case")]
        public List<Case> CaseList { get; set; }

        [YAXSerializeAs("Terminal")]
        [YAXAttributeForClass]
        public Terminal Terminal { get; set; }

    }
}