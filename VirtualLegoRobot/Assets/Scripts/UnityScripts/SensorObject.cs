using System;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UnityScripts
{
    [Serializable]
    public class SensorObject : object
    {
        public GameObject Sensor;
        public SensorTypes Type;
        public object ValueType1, ValueType2;
    }
}