using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class SourceFile
    {
        [YAXSerializeAs("Namespace")]
        public Namespace Namespace { get; set; }
       
    }
}