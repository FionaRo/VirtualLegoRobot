using System;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public class Action
    {
        public static void NewSet(string ports, int speed1, int speed2, double distance, string unitDistance)
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
                        unitType = UnitsTypes.SECONDS;
                        break;
                    case "Rotations":
                        unitType = UnitsTypes.ROTATIONS;
                        break;
                    case "Degrees":
                        unitType = UnitsTypes.DEGREES;
                        break;
                    default:
                        throw new Exception("WTF Assets.Scripts.ProgramScripts.SetAction 32"); //TODO не дай бог сюда попасть
                }
                GlobalVariable.ImportData.Distance = new Distance(distance, unitType);
            }
        }

        public static void NewGet(char port, SensorTypes sensor = SensorTypes.UNKNOWN)
        {
            Get get = new Get()
            {
                Port = port,
                SensorType = sensor
            };
            GlobalVariable.ExportData.Push(get);
        }
    }
}
