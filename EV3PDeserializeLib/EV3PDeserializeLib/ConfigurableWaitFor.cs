﻿using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib 
{
    public class ConfigurableWaitFor : IBlock
    {
        //[YAXSerializeAs("Id")]
        //[YAXAttributeForClass]
        //public string Id { get; set; }

        [YAXSerializeAs("Target")]
        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurablemethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

    }
}