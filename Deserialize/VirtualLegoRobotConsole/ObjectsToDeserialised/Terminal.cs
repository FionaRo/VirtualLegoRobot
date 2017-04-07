using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct Terminal
    {
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass] //Direction of data
        public string Direction { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass] //Connection between 2 blocks
        public string Wire { get; set; }

        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string DataType { get; set; }
    }
}