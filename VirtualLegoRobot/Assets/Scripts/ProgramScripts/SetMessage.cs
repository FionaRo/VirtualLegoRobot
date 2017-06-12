using System;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public static class SetMessage
    {
        public static void SetStartMessage()
        {
            GlobalVariables.Message = "Программа запущена";
        }

        public static void SetMessageMotor(string ports, bool isMove, float speed1, float speed2, float steering, float distance,
            string unitDistance)
        {
            string message;
            if (isMove)
            {
                message = "Движение моторов. Порты: " + ports + ". Мощность: " + speed1 + ". Направление: " + steering + ". Расстояние: ";

            }
            else
            {
                if (ports.Length == 5)
                    message = "Движение моторов. Порты: " + ports + ". Мощность левого: " + speed1 + ". Мощность правого: " + speed2 + ". Расстояние: ";
                else
                    message = "Движение моторов. Порт: " + ports + ". Мощность: " + speed1 + ". Расстояние: ";
            }
            if (unitDistance == null)
            {
                message += "нет.\n";
            }
            else
            {
                message += distance;
                string convertingDistance = Convert.ToString((int)distance);
                int ending;
                switch (convertingDistance[convertingDistance.Length - 1])
                {
                    case '1':
                        ending = 0;
                        break;
                    case '2':
                    case '3':
                    case '4':
                        ending = 1;
                        break;
                    default:
                        ending = 2;
                        break;
                }
                switch (unitDistance)
                {
                    case "Seconds":
                        message += " " + EndingsForMessage.EndingSeconds[ending];
                        break;
                    case "Rotations":
                        message += " " + EndingsForMessage.EndingRotations[ending];
                        break;
                    case "Degrees":
                        message += " " + EndingsForMessage.EndingDegrees[ending];
                        break;
                }
            }
            GlobalVariables.Message = message;
        }

        public static void SetMessageDisplay()
        {
            
        }

        public static void SetMessageIndicator()
        {
            
        }

        public static void SetMessageWait()
        {
            
        }

        public static void SetMessageLoop()
        {
            
        }

        public static void SetMessageSwitch()
        {

        }

        public static void SetMessageInterrupt()
        {

        }

        public static void SetMessageSensor()
        {

        }

        public static void SetMessageData()
        {

        }

        public static void SetMessagePersonal()
        {

        }
    }
}
