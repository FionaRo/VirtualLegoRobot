using System.Collections.Generic;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class Wrap
    {
        public static DeserializedProgram WrapIntoStruct(IRecursiveBlock recBlock)
        {
            Dictionary<string, IBlock> wiresDictionary = new Dictionary<string, IBlock>();
            Dictionary<string, ConfigurableFlatCaseStructure> switchs = new Dictionary<string, ConfigurableFlatCaseStructure>();
            Queue<Wire> turnRunningQueue = new Queue<Wire>();

            if (recBlock.ConfigurableWaitForList != null)
            {
                foreach (var block in recBlock.ConfigurableWaitForList)
                {
                    if (block.TerminalList[1].Wire == null) continue;
                    wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            }

            if (recBlock.ConfigurablemethodCallList != null)
            {
                foreach (var block in recBlock.ConfigurablemethodCallList)
                {
                    if (block.TerminalList[1].Wire == null) continue;
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            }

            if (recBlock.ConfigurableWhileLoopList != null)
            {
                foreach (var block in recBlock.ConfigurableWhileLoopList)
                {
                    if (block.TerminalList[1].Wire == null) continue;
                    wiresDictionary.Add(block.TerminalList[1].Wire, block);
                    block.DeserializedProgram = WrapIntoStruct(block);
                }
            }

            if (recBlock.PairedConfigurableMethodCallList != null)
            {
                foreach (var block in recBlock.PairedConfigurableMethodCallList)
                {
                    if (block.TerminalList[1].Wire == null) continue;
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            }

            var diagram = recBlock as BlockDiagram;
            if (diagram != null)
            {
                var block = diagram.StartBlock;
                if (block.Terminal.Wire != null)
                {
                    wiresDictionary.Add(block.Terminal.Wire, block);
                }
            }
            if (recBlock.ConfigurableFlatCaseStructureList != null)
            {
                foreach (var block in recBlock.ConfigurableFlatCaseStructureList)
                {
                    switchs.Add(block.Id, block);
                    foreach (var caseElement in block.CaseList)
                    {
                        caseElement.DeserializedProgram = WrapIntoStruct(caseElement);
                    }
                }
            }

            foreach (var wire in recBlock.WireList)
            {
                if (wire.Joints.Contains("SequenceOut"))
                {
                    turnRunningQueue.Enqueue(wire);
                }
            }
            DeserializedProgram deserializedProgram = new DeserializedProgram()
            {
                WiresDictionary = wiresDictionary,
                TurnRunning = turnRunningQueue,
                Switch = switchs
            };
            return deserializedProgram;
        }
    }
}
