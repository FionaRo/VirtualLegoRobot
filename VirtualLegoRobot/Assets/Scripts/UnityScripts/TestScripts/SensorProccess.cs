using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UnityScripts.TestScripts
{
    public class SensorProcess : MonoBehaviour
    {
        public Transform Screen;
        private float _maxDistance = 150 * 11f;

        void Start()
        {

        }

        void Update()
        {
            foreach (var el in SensorData.SensorPorts)
            {
                if (el == null) continue;
                switch (el.Type)
                {
                    case SensorTypes.ColorSensor:
                        ColorProc(el);
                        break;
                    case SensorTypes.TouchSensor:
                        TouchProc(el);
                        break;
                    case SensorTypes.InfraredSensor:
                    case SensorTypes.UltrasonicSensorCm:
                        DistanceProc(el);
                        break;
                }
            }
            foreach (var el in SensorData.MotorPorts)
            {
                if (el == null) continue;
                MotorProc(el);
            }
            for (int i = 0; i < SensorData.Timer.Length; i++)
            {
                SensorData.Timer[i] += Time.deltaTime;
            }
        }

        void DistanceProc(SensorObject sensor)
        {
            Transform camera1 = sensor.Sensor.transform.GetChild(1).transform;
            Transform camera2 = sensor.Sensor.transform.GetChild(2).transform;
            RaycastHit hit1, hit2;
            sensor.ValueType1 = _maxDistance / 11;
            sensor.ValueType2 = _maxDistance / 11 * 0.393701;

            bool h1 = Physics.Raycast(camera1.position, camera1.forward, out hit1, _maxDistance);
            bool h2 = Physics.Raycast(camera2.position, camera2.forward, out hit2, _maxDistance);
            if (h1 && h2) hit1 = hit1.distance < hit2.distance ? hit1 : hit2;
            else if (h2) hit1 = hit2;

            sensor.ValueType1 = hit1.distance / 11;
            sensor.ValueType2 = hit1.distance / 11 * 0.393701;
        }

        void TouchProc(SensorObject sensor)
        {
            RaycastHit hit;
            Transform sensorCamera = sensor.Sensor.transform.parent.GetChild(1);
            if (Physics.Raycast(sensorCamera.position, sensorCamera.forward, out hit, 5f))
            {
                sensor.ValueType1 = (int)ButtonConditions.Pressed;
            }
            else
            {
                sensor.ValueType1 = (int)ButtonConditions.Realised;
            }
        }

        void ColorProc(SensorObject sensor)
        {
            RaycastHit hit;
            Transform sensorCamera = sensor.Sensor.transform.GetChild(1);
            SensorColors sensorColor = SensorColors.None;
            if (Physics.Raycast(sensorCamera.position, sensorCamera.forward, out hit, sensorCamera.GetComponent<Camera>().farClipPlane))
            {
                Color? color = hit.transform.GetComponent<MeshRenderer>().material.color;
                if (color == Color.black) sensorColor = SensorColors.Black;
                else if (color == Color.blue) sensorColor = SensorColors.Blue;
                else if (color == Color.green) sensorColor = SensorColors.Green;
                else if (color == Color.white || color == Color.gray) sensorColor = SensorColors.White;
                else if (color == Color.yellow) sensorColor = SensorColors.Yellow;
                else if (color == Color.red) sensorColor = SensorColors.Red;
                else sensorColor = SensorColors.Black;
            }
            sensor.ValueType1 = sensorColor;
        }

        void MotorProc(SensorObject motor)
        {
            var motorCollider = motor.Sensor.transform.GetComponent<WheelCollider>();
            motor.ValueType1 = motorCollider.rpm / 60 * Time.deltaTime;
        }
    }
}