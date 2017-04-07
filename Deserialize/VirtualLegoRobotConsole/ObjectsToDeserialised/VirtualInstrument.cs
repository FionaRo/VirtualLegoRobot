using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct VirtualInstrument
    {
        [YAXSerializeAs("IsTopLevel")]
        [YAXAttributeForClass]
        public string IsTopLevel { get; set; }

        //[YAX]
        [YAXSerializeAs("BlockDiagram")]
        public BlockDiagram BlockDiagram { get; set; }
    }
}