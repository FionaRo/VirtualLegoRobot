using YAXLib;

namespace EV3PDeserializeLib
{
      public struct Namespace
    {
        [YAXSerializeAs("Name")]
        [YAXAttributeForClass]
        public string ProjectName { get; set; }

        [YAXSerializeAs("VirtualInstrument")]
        public VirtualInstrument VirtualInstrument { get; set; }

    }
}
