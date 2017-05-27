using YAXLib;

namespace EV3PDeserializeLib
{
    public class Wire
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("Joints")]
        [YAXAttributeForClass]
        public string Joints { get; set; }
    }
}
