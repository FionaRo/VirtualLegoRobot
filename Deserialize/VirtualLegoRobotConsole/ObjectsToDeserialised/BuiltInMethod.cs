using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public struct BuiltInMethod
    {
        [YAXAttributeForClass]
        public string CallType { get; set; }

       public ConfigurableMethodCall ConfigurableMethodCall { get; set; }


    }
}
