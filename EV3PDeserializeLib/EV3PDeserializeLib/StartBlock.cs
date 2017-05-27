using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class StartBlock : IBlock
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