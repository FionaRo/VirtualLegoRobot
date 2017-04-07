using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableMethodCall
    {
        [YAXSerializeAs("ConfigurableMethodTerminal")]
        [YAXDontSerialize]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }

        [YAXSerializeAs("Terminal")]
        [YAXDontSerialize]
        public List<Terminal> TerminalList { get; set; }

    }
}