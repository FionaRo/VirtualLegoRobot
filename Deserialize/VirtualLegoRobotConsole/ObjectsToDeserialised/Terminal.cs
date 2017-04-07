using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct Terminal
    {
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXAttributeForClass] //Direction of data
        public string Direction { get; set; }

        //[YAXAttributeForClass] //Connection between 2 blocks
        //public string Wire { get; set; }

        [YAXAttributeForClass]
        public string DataType { get; set; }
    }
}