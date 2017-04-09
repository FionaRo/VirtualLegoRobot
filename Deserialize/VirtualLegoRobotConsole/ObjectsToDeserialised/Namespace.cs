using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class Namespace
    {
        [YAXSerializeAs("Name")]
        [YAXAttributeForClass]
        public string ProjectName { get; set; }

        [YAXSerializeAs("VirtualInstrument")]
        public VirtualInstrument VirtualInstrument { get; set; }

    }
}
