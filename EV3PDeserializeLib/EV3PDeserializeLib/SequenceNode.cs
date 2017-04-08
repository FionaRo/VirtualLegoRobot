using YAXLib;

namespace EV3PDeserializeLib
{
    public struct SequenceNode
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }
        
        [YAXSerializeAs("Terminal")]
        public Terminal Terminal { get; set; }
    }
}
