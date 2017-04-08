using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableMethodCall
    {
        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

    }
}