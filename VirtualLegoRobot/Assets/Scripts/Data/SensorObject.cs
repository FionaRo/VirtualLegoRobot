using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class SensorObject : object
    {
        public GameObject Sensor;
        public SensorTypes Type;
        public object ValueType1, ValueType2, ValueType3;
    }
}