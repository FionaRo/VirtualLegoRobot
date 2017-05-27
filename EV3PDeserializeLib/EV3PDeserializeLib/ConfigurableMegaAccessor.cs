using System.Collections.Generic;
using YAXLib;

namespace EV3PDeserializeLib
{
    public class ConfigurableMegaAccessor
    {
        [YAXSerializeAs("AccessorType")]
        [YAXAttributeForClass]
        public string AccessorType { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodTerminal")]
        public List<ConfigurableMethodTerminal> ConfigurableMethodTerminalList { get; set; }
    }
}