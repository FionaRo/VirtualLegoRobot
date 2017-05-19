using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3PDeserializeLib.Action
{
    class Set
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
