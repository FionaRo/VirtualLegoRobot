using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace EV3PDeserializeLib
{
    public struct BuiltInMethod
    {
        [YAXSerializeAs("CallType")]
        [YAXAttributeForClass]
        public string CallType { get; set; }

        [YAXSerializeAs("ConfigurableMethodCall")]
        public ConfigurableMethodCall ConfigurableMethodCall { get; set; }

    }
}
