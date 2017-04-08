using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableFlatCaseStructure
    {
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXAttributeForClass]
        [YAXSerializeAs("PairedConfigurableMethodCall")]
        public string ParentId { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableFlatCaseStructure.Case")]
        public List<Case> CaseList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }
    }
}