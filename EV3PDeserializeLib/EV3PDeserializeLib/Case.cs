using YAXLib;
using System.Collections.Generic;

namespace EV3PDeserializeLib
{ 
    public struct Case
    {
        [YAXSerializeAs("Pattern")]
        [YAXAttributeForClass]
        public string Pattern { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "SequenceNode")]
        public List<SequenceNode> SequenceNodeList { get; set; }

        [YAXSerializeAs("Wire")]
        public Wire Wire { get; set; }
    }
}
