using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class Wire
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }
    }
}
