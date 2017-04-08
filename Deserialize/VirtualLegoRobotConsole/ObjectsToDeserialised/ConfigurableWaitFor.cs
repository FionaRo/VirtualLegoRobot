using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableWaitFor
    {
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurablemethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }
    }
}