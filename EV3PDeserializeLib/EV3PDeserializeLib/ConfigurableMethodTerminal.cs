using YAXLib;

namespace EV3PDeserializeLib
{
    public class ConfigurableMethodTerminal
    {
        [YAXSerializeAs("ConfiguredValue")]
        [YAXAttributeForClass]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        public string ConfiguredValue { get; set; }

        [YAXSerializeAs("Terminal")]
        public Terminal Terminal { get; set; }
    }
}