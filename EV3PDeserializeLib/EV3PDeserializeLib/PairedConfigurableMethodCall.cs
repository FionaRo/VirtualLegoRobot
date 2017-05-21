using System;
using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class PairedConfigurableMethodCall : IBlock
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("Target")]
        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXSerializeAs("PairedStructure")]
        [YAXAttributeForClass]
        public string PairedStructure { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurablemethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

    }
}