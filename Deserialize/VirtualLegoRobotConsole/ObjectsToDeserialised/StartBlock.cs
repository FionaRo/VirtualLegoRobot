using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct StartBlock
    {
        [YAXAttributeForClass]
        public string Id { get; set; }

        //[YAXSerializeAs("ConfigurableMethodTerminal")]
        public ConfigurableMethodTerminal ConfigurableMethodTerminal { get; set; }
    }
}