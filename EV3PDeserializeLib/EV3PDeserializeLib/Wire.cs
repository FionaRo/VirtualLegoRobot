using YAXLib;

namespace EV3PDeserializeLib
{
    public class Wire
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }
    }
}
