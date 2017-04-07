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
        //[YAXSerializeAs("StartBlock")] //Default start block (If it doesn't exist - program is nonworking) 
        public StartBlock StartBlock { get; set; }

        [YAXDontSerialize]
        [YAXSerializeAs("Wire")] //Connections between blocks
        public List<Wire> WireList { get; set; }

        [YAXDontSerialize]
        [YAXSerializeAs("ConfigurableMethodCall")] //Blocks of action
        public List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        [YAXDontSerialize]
        [YAXSerializeAs("ConfigurableWaitFors")] //Waiting blocks
        public List<ConfigurableWaitFors> ConfigurableWaitForsList { get; set; }

        [YAXDontSerialize]
        [YAXSerializeAs("PairedConfigurableMethodCall")] //Data of switching
        public List<PairedConfigurableMethodCall> PairedConfigurableMethodCallList { get; set; }
        [YAXDontSerialize]
        [YAXSerializeAs("ConfigurableFlatCaseStructure")] //Condition of switch
        public List<ConfigurableFlatCaseStructure> ConfigurableFlatCaseStructureList { get; set; }

        [YAXDontSerialize]
        [YAXSerializeAs("ConfigurableWhileLoop")] //Loop
        public List<ConfigurableWhileLoop> ConfigurableWhileLoopList { get; set; }
    }
}
