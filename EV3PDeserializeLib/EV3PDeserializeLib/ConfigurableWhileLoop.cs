using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class ConfigurableWhileLoop : IRecursiveBlock, IBlock
    {
        [YAXDontSerialize]
        public DeserializedProgram DeserializedProgram { get; set; }

        //[YAXSerializeAs("Id")]
        //[YAXAttributeForClass]
        //public string Id { get; set; }

        [YAXSerializeAs("InterruptName")]
        [YAXAttributeForClass]
        public string InterruptName { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Terminal")]
        public List<Terminal> TerminalList { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWhileLoop.BuiltInMethod")]
        public List<BuiltInMethod> BuiltInMethod { get; set; }

        //Связи между блоками. Указывает на последовательность блоков по их id
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Wire")]
        public List<Wire> WireList { get; set; }

        //Блоки действий
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodCall")]
        public List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        //Блоки ожидания
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWaitFor")]
        public List<ConfigurableWaitFor> ConfigurableWaitForList { get; set; }

        //Switch (переключатель, ветвление). Две связанных структуры - условие и разветвления
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "PairedConfigurableMethodCall")]
        public List<PairedConfigurableMethodCall> PairedConfigurableMethodCallList { get; set; }
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableFlatCaseStructure")]
        public List<ConfigurableFlatCaseStructure> ConfigurableFlatCaseStructureList { get; set; }

        //Блок цикла
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWhileLoop")]
        public List<ConfigurableWhileLoop> ConfigurableWhileLoopList { get; set; }
    }
}