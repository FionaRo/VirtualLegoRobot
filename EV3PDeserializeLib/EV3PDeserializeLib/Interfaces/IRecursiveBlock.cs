using System.Collections.Generic;
using EV3PDeserializeLib.Interfaces;
using YAXLib;

namespace EV3PDeserializeLib
{
    public interface IRecursiveBlock
    {
        //Связи между блоками. Указывает на последовательность блоков по их id
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "Wire")]
         List<Wire> WireList { get; set; }

        //Блоки действий
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableMethodCall")]
         List<ConfigurableMethodCall> ConfigurablemethodCallList { get; set; }

        //Блоки ожидания
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWaitFor")]
         List<ConfigurableWaitFor> ConfigurableWaitForList { get; set; }

        //Switch (переключатель, ветвление). Две связанных структуры - условие и разветвления
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "PairedConfigurableMethodCall")]
         List<PairedConfigurableMethodCall> PairedConfigurableMethodCallList { get; set; }
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableFlatCaseStructure")]
         List<ConfigurableFlatCaseStructure> ConfigurableFlatCaseStructureList { get; set; }

        //Блок цикла
        [YAXCollection(YAXCollectionSerializationTypes.RecursiveWithNoContainingElement, EachElementName = "ConfigurableWhileLoop")]
         List<ConfigurableWhileLoop> ConfigurableWhileLoopList { get; set; }

        DeserializedProgram DeserializedProgram { get; set; }

    }
}
