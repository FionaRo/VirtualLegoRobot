using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableMethodCall
    {
        [YAXSerializeAs("ConfigurableMethodTerminal")]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }

        [YAXSerializeAs("Terminal")]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        public List<Terminal> TerminalList { get; set; }

    }
}