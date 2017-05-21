using YAXLib;
using System.Collections.Generic;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{ 
    public class Case : IRecursiveBlock
    {
        public DeserializedProgram DeserializedProgram { get; set; }

        [YAXSerializeAs("Pattern")]
        [YAXAttributeForClass]
        public string Pattern { get; set; }

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "SequenceNode")]
        public List<SequenceNode> SequenceNodeList { get; set; }

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
