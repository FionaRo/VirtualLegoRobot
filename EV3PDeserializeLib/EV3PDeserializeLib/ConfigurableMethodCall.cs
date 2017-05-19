using System;
using System.Collections.Generic;
using YAXLib;

namespace EV3PDeserializeLib
{
    public struct ConfigurableMethodCall : IBlock, IBaseAction
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("Target")]
        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

        public string GetBlockName()
        {
            return Target;
        }

        public string GetId()
        {
            return Id;
        }
    }
}