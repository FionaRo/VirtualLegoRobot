using System;
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
            if (recBlock.ConfigurableWaitForList != null)
                foreach (var block in recBlock.ConfigurableWaitForList)
                {
                    if (block.TerminalList[1].Wire != null)
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            if (recBlock.ConfigurablemethodCallList != null)
                foreach (var block in recBlock.ConfigurablemethodCallList)
                {
                    if (block.TerminalList[1].Wire != null)
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            if (recBlock.ConfigurableWhileLoopList != null)
                foreach (var block in recBlock.ConfigurableWhileLoopList)
                {
                    if (block.TerminalList[1].Wire != null)
                    {
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                        block.DeserializedProgram = WrapIntoStruct(block);
                    }
                }
            if (recBlock.PairedConfigurableMethodCallList != null)
                foreach (var block in recBlock.PairedConfigurableMethodCallList)
                {
                    if (block.TerminalList[1].Wire != null)
                        wiresDictionary.Add(block.TerminalList[1].Wire, block);
                }
            if (recBlock is BlockDiagram)
            {
                var block = ((BlockDiagram)recBlock).StartBlock;
                if (block.Terminal.Wire != null)
                    wiresDictionary.Add(block.Terminal.Wire, block);
            }
            if (recBlock.ConfigurableFlatCaseStructureList != null)
                foreach (var block in recBlock.ConfigurableFlatCaseStructureList)
                {
                    switchs.Add(block.Id, block);
                    foreach(var caseElement in block.CaseList)
                    {
                        caseElement.DeserializedProgram = WrapIntoStruct(caseElement);
                    }
                }
            DeserializedProgram deserializedProgram = new DeserializedProgram()
            {
                WiresDictionary = wiresDictionary,
                TurnRunning = recBlock.WireList,
                Switch = switchs
            };
            return deserializedProgram;
        }
    }
}
