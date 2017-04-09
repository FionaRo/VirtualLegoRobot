using YAXLib;

namespace EV3PDeserializeLib
{
    public struct StartBlock
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("ConfigurableMethodTerminal")]
        public ConfigurableMethodTerminal ConfigurableMethodTerminal { get; set; }

        [YAXSerializeAs("Terminal")]
        public Terminal Terminal { get; set; }
    }
}