using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class StartBlock
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