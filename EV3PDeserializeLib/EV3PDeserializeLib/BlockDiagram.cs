using System;
using System.Collections.Generic;
using YAXLib;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public class BlockDiagram : IRecursiveBlock
    {
        [YAXDontSerialize]
        public DeserializedProgram DeserializedProgram { get; set; }

        //Стартовый блок. Если элемент отсутствует - кидает предупреждение и не десериализует файл (нет смысла - программа не будет выполняться)
        [YAXSerializeAs("StartBlock")] 
        [YAXErrorIfMissed(YAXExceptionTypes.Warning)]
        public StartBlock StartBlock { get; set; }

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

        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMegaAccessor")]
        public List<ConfigurableMegaAccessor> ConfigurableMegaAccessor { get; set; }

    }
}
