using YAXLib;

namespace EV3PDeserializeLib
{
    public struct VirtualInstrument
    {
        [YAXSerializeAs("BlockDiagram")]
        public BlockDiagram BlockDiagram { get; set; }

    }
}