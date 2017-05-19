using System.Collections.Generic;
using System.IO;
using YAXLib;

namespace EV3PDeserializeLib
{
    public struct DeserializedProgram
    {
        public Dictionary<string, IBlock> WiresDictionary { get; set; }
        public Dictionary<string, ConfigurableFlatCaseStructure> Switch { get; set; }
        public List<Wire> TurnRunning { get; set; }
    }

    public class SourceFile
    {
        [YAXSerializeAs("Namespace")]
        public Namespace Namespace { get; set; }


        public static DeserializedProgram Deserialize(string filename)
        {
            YAXSerializer serializer = new YAXSerializer(typeof(SourceFile));
            try
            {
                object deserializedObject = serializer.DeserializeFromFile(filename);
                if (deserializedObject != null)
                {
                    SourceFile sourceFile = (SourceFile)deserializedObject;
                    Dictionary<string, IBlock> wiresDictionary = new Dictionary<string, IBlock>();
                    Dictionary<string, ConfigurableFlatCaseStructure> switchs = new Dictionary<string, ConfigurableFlatCaseStructure>();
                    foreach (var block in sourceFile.Namespace.VirtualInstrument.BlockDiagram.ConfigurableWaitForList)
                    {
                        wiresDictionary.Add(block.TerminalList[0].Wire, block);
                    }
                    foreach (var block in sourceFile.Namespace.VirtualInstrument.BlockDiagram.ConfigurablemethodCallList)
                    {
                        wiresDictionary.Add(block.TerminalList[0].Wire, block);
                    }
                    foreach (var block in sourceFile.Namespace.VirtualInstrument.BlockDiagram.ConfigurableWhileLoopList)
                    {
                        wiresDictionary.Add(block.TerminalList[0].Wire, block);
                    }
                    foreach (var block in sourceFile.Namespace.VirtualInstrument.BlockDiagram.PairedConfigurableMethodCallList)
                    {
                        wiresDictionary.Add(block.TerminalList[0].Wire, block);
                    }
                    {
                        var block = sourceFile.Namespace.VirtualInstrument.BlockDiagram.StartBlock;
                        wiresDictionary.Add(block.Terminal.Wire, block);
                    }
                    foreach (var block in sourceFile.Namespace.VirtualInstrument.BlockDiagram.ConfigurableFlatCaseStructureList)
                    {
                        switchs.Add(block.Id, block);
                    }
                    DeserializedProgram deserializedProgram = new DeserializedProgram()
                    {
                        WiresDictionary = wiresDictionary,
                        TurnRunning = sourceFile.Namespace.VirtualInstrument.BlockDiagram.WireList,
                        Switch = switchs
                    };
                    return deserializedProgram;
                }
                else
                {
                    throw new IOException("Deserializing XML is failed");
                }
            }
            catch
            {
                throw new IOException("Deserializing XML is failed");
            }
        }
    }
}
