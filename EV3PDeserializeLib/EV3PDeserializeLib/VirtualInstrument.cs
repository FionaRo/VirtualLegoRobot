using YAXLib;

namespace EV3PDeserializeLib
{
    public class VirtualInstrument
    {
        [YAXSerializeAs("BlockDiagram")]
        public BlockDiagram BlockDiagram { get; set; }

    }
}