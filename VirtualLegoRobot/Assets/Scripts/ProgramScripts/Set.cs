using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProgramScripts
{
    public class Set
    {
        public Motor Motor1 { get; set; }
        public Motor Motor2 { get; set; }
        public Distance Distance { get; set; }

        public Set()
        {
            Motor1 = Motor2 = null;
            Distance = null;
        }
    }
}
