using YAXLib;

namespace EV3PDeserializeLib
{
    public class Terminal
    {
        [YAXSerializeAs("Id")]
        [YAXAttributeForClass]
        public string Id { get; set; }

        [YAXSerializeAs("Direction")] 
        [YAXAttributeForClass] 
        public string Direction { get; set; }

        [YAXSerializeAs("DataType")]
        [YAXAttributeForClass]
        public string DataType { get; set; }

        [YAXSerializeAs("Wire")]
        [YAXErrorIfMissed(YAXExceptionTypes.Ignore)]
        [YAXAttributeForClass]
        public string Wire { get; set; }

    }
}