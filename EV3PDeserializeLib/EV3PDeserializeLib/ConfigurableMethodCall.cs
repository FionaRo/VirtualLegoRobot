using System;
using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class ConfigurableMethodCall : IBlock
    {
        //[YAXSerializeAs("Id")] //А надо ли?
        //[YAXAttributeForClass]
        //public string Id { get; set; }

        [YAXSerializeAs("Target")] //Имя 
        [YAXAttributeForClass]
        public string Target { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")] //Id для Wire
        public List<Terminal> TerminalList { get; set; }

    }
}