using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct VirtualInstrument
    {
        [YAXSerializeAs("BlockDiagram")]
        public BlockDiagram BlockDiagram { get; set; }

    }
}