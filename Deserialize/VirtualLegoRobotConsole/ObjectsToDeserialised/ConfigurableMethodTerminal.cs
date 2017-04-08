using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableMethodTerminal
    {
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string Id { get; set; } //????? TODO

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string ConfiguredValue { get; set; }

        public Terminal Terminal { get; set; }
    }
}