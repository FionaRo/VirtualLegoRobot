

using System;

namespace Assets.Scripts.ProgramScripts
{
    public class SetAction
    {
        public static void NewSet(string ports, int speed1 = 0, int speed2 = 0, int distance = 0, string unitDistance = null)
        {
            GlobalVariable.ImportData = new Set();
            GlobalVariable.ImportData.Motor1 = new Motor(ports[2], speed1);
            if (ports.Length == 5) // 1.A+B
            {
                GlobalVariable.ImportData.Motor2 = new Motor(ports[4], speed2);
            }
            if (unitDistance != null)
            {
                UnitsTypes unitType;
                switch (unitDistance)
                {
                    case "Seconds":
                        unitType = UnitsTypes.TIME;
                        break;
                    case "Rotations":
                        unitType = UnitsTypes.ROTATE;
                        break;
                    case "Degrees":
                        unitType = UnitsTypes.DEGREE;
                        break;
                    default:
                        throw new Exception("WTF Assets.Scripts.ProgramScripts.SetAction 32"); //TODO не дай бог сюда попасть
                }
                GlobalVariable.ImportData.Distance = new Distance(distance, unitType);
            }
        }
    }
}
