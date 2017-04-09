using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class VirtualInstrument
    {
        [YAXSerializeAs("BlockDiagram")]
        public BlockDiagram BlockDiagram { get; set; }

    }
}