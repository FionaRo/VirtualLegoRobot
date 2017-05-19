using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3PDeserializeLib.Action
{
    class Motor
    {
        public char Port { get; }
        public int Power { get; }

        public Motor(char port, int power)
        {
            Port = port;
            Power = power;
        }
    }
}
