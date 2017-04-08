using YAXLib;
namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct SequenceNode
    {
        [YAXAttributeForClass]
        public string Id { get; set; }
        
        public Terminal Terminal { get; set; }
    }
}
