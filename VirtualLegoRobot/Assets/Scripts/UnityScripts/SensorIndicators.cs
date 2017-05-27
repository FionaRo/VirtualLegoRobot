using Assets.Scripts.Data;

namespace Assets.Scripts.UnityScripts
{
    public static class SensorIndicators
    {
        public static int Color = 0;
        public static int ReflectedLight = 0;
        public static int IrProximity = 0;
        public static int Ultrasonic = 0;
        public static int LargeMotor1 = 0;
        public static int LargeMotor2 = 0;
        public static double[] Timer = new double[9];
        public static ButtonConditions[] Buttons = new ButtonConditions[5];
    }
}
