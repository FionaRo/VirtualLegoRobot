using YAXLib;
namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class SequenceNode
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("Terminal")]
        public Terminal Terminal { get; set; }
    }
}
