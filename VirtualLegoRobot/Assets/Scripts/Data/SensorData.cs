using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public static class SensorData
    {
        public static GameObject Prefab;
        public static SensorObject[] SensorPorts = {null, null, null, null};
        public static SensorObject[] MotorPorts = {null, null, null, null};
        public static float[] Timer = new float[9];
        public static ButtonConditions[] Buttons = new ButtonConditions[5];
    }
}