using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct Wire
    {
        //[YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string Id { get; set; }
    }
}
