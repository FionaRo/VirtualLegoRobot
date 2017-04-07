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

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("Wire")] //Connections between blocks
        public List<Wire> WireList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("ConfigurableMethodCall")] //Blocks of action
        public List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("ConfigurableWaitFors")] //Waiting blocks
        public List<ConfigurableWaitFors> ConfigurableWaitForsList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("PairedConfigurableMethodCall")] //Data of switching
        public List<PairedConfigurableMethodCall> PairedConfigurableMethodCallList { get; set; }
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("ConfigurableFlatCaseStructure")] //Condition of switch
        public List<ConfigurableFlatCaseStructure> ConfigurableFlatCaseStructureList { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXSerializeAs("ConfigurableWhileLoop")] //Loop
        public List<ConfigurableWhileLoop> ConfigurableWhileLoopList { get; set; }
    }
}
