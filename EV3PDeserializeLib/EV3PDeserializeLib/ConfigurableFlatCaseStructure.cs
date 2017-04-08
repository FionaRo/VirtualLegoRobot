using System.Collections.Generic;
using YAXLib;

namespace EV3PDeserializeLib
{
    public struct ConfigurableFlatCaseStructure
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