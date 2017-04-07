using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct StartBlock
    {
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string Id { get; set; }

        //[YAXSerializeAs("ConfigurableMethodTerminal")]
        public ConfigurableMethodTerminal ConfigurableMethodTerminal { get; set; }
    }
}