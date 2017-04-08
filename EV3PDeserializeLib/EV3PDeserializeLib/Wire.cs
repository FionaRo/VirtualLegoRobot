using YAXLib;

namespace EV3PDeserializeLib
{
    public struct Wire
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }
    }
}
