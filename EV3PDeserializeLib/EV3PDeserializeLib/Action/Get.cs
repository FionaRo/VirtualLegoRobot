using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3PDeserializeLib.Action
{
    enum Types { BIG_MOTOR, MID_MOTOR, LIGHT, BUTTON } // ДОПИСАТЬ
    class Get
    {
        public char Port { get; set; }
        public Types Type { get; set; } 
        public int Value { get; set; }
    }
}
