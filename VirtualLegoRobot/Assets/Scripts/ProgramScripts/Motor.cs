using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProgramScripts
{
    public class Motor
    {
        public char Port { get; set; }
        public int Power { get; set; }

        public Motor(char port, int power)
        {
            Port = port;
            Power = power;
        }
    }
}
