using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct BlockDiagram
    {

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("StartBlock")] //Default start block (If it doesn't exist - program is nonworking) 
        public StartBlock StartBlock { get; set; }

        //[YAXSerializeAs("Wire")] //Connections between blocks
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Wire")]
        public List<Wire> WireList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodCall")]
        public List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWaitFor")]
        public List<ConfigurableWaitFor> ConfigurableWaitForList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "PairedConfigurableMethodCall")]
        public List<PairedConfigurableMethodCall> PairedConfigurableMethodCallList { get; set; }
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableFlatCaseStructure")]
        public List<ConfigurableFlatCaseStructure> ConfigurableFlatCaseStructureList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWhileLoop")]
        public List<ConfigurableWhileLoop> ConfigurableWhileLoopList { get; set; }
    }
}
