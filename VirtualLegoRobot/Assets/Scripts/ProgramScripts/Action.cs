using System;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public class Action
    {
        public static void NewSetMotor(string ports, float? speed1, float? speed2, float? distance, string unitDistance)
        {
            GlobalVariables.ImportData = new Set
            {
                Motor1 = new Motor(ports[2], speed1.Value),
                SetType = SetTypes.LargeMotor
            };
            if (ports.Length == 5) // 1.A+B
            {
                GlobalVariables.ImportData.Motor2 = new Motor(ports[4], speed2.Value);
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
            GlobalVariables.ImportData.Distance = new Distance(distance.Value, unitType);
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
         float x1, float y1,
         float x2, float y2,
         bool clearScreen,
         string text,
         bool invert,
         int size,
         float radius,
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
            bool isWait,
            SensorTypes sensor,
            bool isComparasion,
            char port = '0',
            ComparasionTypes comparasion = ComparasionTypes.Equal,
            Variable valueCompare = null,
            Variable arrayCompare = null, Variable buttons = null, Variable timer = null,
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
            };
            GlobalVariables.ExportData.Push(get);
        }
    }
}
