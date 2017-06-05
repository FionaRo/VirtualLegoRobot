using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UnityScripts
{
    [Serializable]
    public static class SensorData
    {
        public static GameObject Prefab;
        public static SensorObject[] SensorPorts = {null, null, null, null};
        public static SensorObject[] MotorPorts = {null, null, null, null};
        public static double[] Timer = new double[9];
        public static ButtonConditions[] Buttons = new ButtonConditions[5];
    }
}