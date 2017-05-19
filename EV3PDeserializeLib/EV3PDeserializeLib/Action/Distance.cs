using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3PDeserializeLib.Action
{
    public enum Units { TIME, ROTATE, DEGREE}

    class Distance
    {
        public int Value { get; }
        Units Unit { get; }

        public Distance(int value, Units unit) { Value = value; Unit = unit; }
    }
}
