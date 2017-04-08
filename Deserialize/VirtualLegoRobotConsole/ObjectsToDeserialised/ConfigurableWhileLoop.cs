using System.Collections.Generic;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct ConfigurableWhileLoop
    {

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWhileLoop.BuiltInMethod")]
        public List<BuiltInMethod> BuiltInMethod { get; set; }
        
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodCall")]
        public List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Wire")]
        public List<Wire> WireList { get; set; }
    }
}