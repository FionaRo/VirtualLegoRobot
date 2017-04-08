using YAXLib;
using System.Collections.Generic;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct Case
    {
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXAttributeForClass]
        public string Pattern { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "SequenceNode")]
        public List<SequenceNode> SequenceNodeList { get; set; }

        public Wire Wire { get; set; }
    }
}
