using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EV3PDeserializeLib.Action
{
    class SetAction
    {
        Set set; //global

        public SetAction(string ports, int speed1 = 0, int speed2 = 0, int dist = 0, string unitDistance = null)
        {
            set = new Set();
            set.Motor1 = new Motor(ports[2], speed1);
            if (ports.Length == 5)
            {
                set.Motor2 = new Motor(ports[2], speed2);
            }
            if (unitDistance!=null)
            {
                Units unit;
                switch(unitDistance)
                {
                    case "Seconds":
                        unit = Units.TIME;
                        break;
                    case "Rotations":
                        unit = Units.ROTATE;
                        break;
                    case "Degrees":
                        unit = Units.DEGREE;
                        break;
                    default:
                        throw new Exception("WTF SetAction 36");
                }
                set.Distance = new Distance(dist, unit);
            }
        }
    }
}
