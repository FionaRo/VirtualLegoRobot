using System;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public class Action
    {
        public static void NewSetMotor(string ports, double speed1, double speed2, double distance, string unitDistance)
        {
            GlobalVariables.ImportData = new Set
            {
                Motor1 = new Motor(ports[2], speed1),
                SetType = SetTypes.LargeMotor
            };
            if (ports.Length == 5) // 1.A+B
            {
                GlobalVariables.ImportData.Motor2 = new Motor(ports[4], speed2);
            }
            if (unitDistance == null) return;

            UnitTypes unitType = new UnitTypes();
            switch (unitDistance)
            {
                case "Seconds":
                    unitType = UnitTypes.Seconds;
                    break;
                case "Rotations":
                    unitType = UnitTypes.Rotations;
                    break;
                case "Degrees":
                    unitType = UnitTypes.Degrees;
                    break;
                default:
                    throw new Exception("Unexpected UnitType");
            }
            GlobalVariables.ImportData.Distance = new Distance(distance, unitType);
        }

        public static void NewSetIndicator(int color, bool impulse)
        {
            GlobalVariables.ImportData = new Set()
            {
                SetType = SetTypes.Indicator,
                Indicator = new Indicator()
                {
                    Impulse = impulse,
                    IndicatorColor = (IndicatorColors)color
                },
            };
        }

        public static void NewSetDisplay(DisplayTypes displayType,
         double x1, double y1,
         double x2, double y2,
         bool clearScreen,
         string text,
         bool invert,
         int size,
         double radius,
         bool fill)
        {
            Display display = new Display
            {
                DisplayType = displayType,
                X1 = x1,
                Y1 = y1,
                ClearScreen = clearScreen,
                Invert = invert
            };
            switch (displayType)
            {
                case DisplayTypes.Text:
                case DisplayTypes.TextGrid:
                    display.Text = text;
                    display.Size = size;
                    break;
                case DisplayTypes.Line:
                    display.X2 = x2;
                    display.Y2 = y2;
                    break;
                case DisplayTypes.Rect:
                    display.X2 = x2;
                    display.Y2 = y2;
                    display.Fill = fill;
                    break;
                case DisplayTypes.Point:
                    break;
                case DisplayTypes.Circle:
                    display.Fill = fill;
                    display.Radius = radius;
                    break;
            }
            GlobalVariables.ImportData = new Set()
            {
                Display = display,
                SetType = SetTypes.Display
            };
        }

        public static void NewGet(
            Boolean isWait,
            SensorTypes sensor,
            bool isComparasion,
            char port = '0',
            ComparasionTypes comparasion = ComparasionTypes.Equal,
            double valueCompare = 0,
            int[] arrayCompare = null, int[] buttons = null, int timer = 0,
            bool reset = false)
        {
            Get get = new Get()
            {
                IsWait = isWait,
                Port = port,
                SensorType = sensor,
                IsComparsion = isComparasion,
                Comparasion = comparasion,
                ValueCompare = valueCompare,
                ArrayCompare = arrayCompare,
                Buttons = buttons,
                Timer = timer,
                CanContinue = false,
                Reset = reset
            };
            GlobalVariables.ExportData.Push(get);
        }
    }
}
