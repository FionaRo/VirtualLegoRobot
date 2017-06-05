using System;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class SensorEdit : MonoBehaviour
    {
        private Material _material;
        private bool _choosen;
        private readonly Color _originColor = Color.gray;
        private readonly Color _choosenColor = Color.cyan;

        public float SpeedMove = 30f;
        public float SpeedRotate = 10f;

        public KeyCode Left = KeyCode.LeftArrow;
        public KeyCode Right = KeyCode.RightArrow;
        public KeyCode Back = KeyCode.UpArrow;
        public KeyCode Forward = KeyCode.DownArrow;
        public KeyCode Delete = KeyCode.Delete;
        public KeyCode SwitchCode = KeyCode.LeftAlt;
        public KeyCode Up = KeyCode.KeypadPlus;
        public KeyCode Down = KeyCode.KeypadEnter;
        public KeyCode One = KeyCode.Alpha1;
        public KeyCode Two = KeyCode.Alpha2;
        public KeyCode Three = KeyCode.Alpha3;
        public KeyCode Four = KeyCode.Alpha4;

        private Transform _thisObject;
        private SensorObject _sensorObject;

        private float _maxHeight = 100f;
        private float _minHeight;
        private float _x, _y;
        private float _height;
        private float _tmpHeight;
        private float _rotationX;
        private float _rotationY;
        private float _rotationZ;
        private int _port = 5;
        private int _id;

        void Start()
        {
            _id = ++AddItemToField.Count;
            _material = new Material(Shader.Find("Specular"));
            GetComponentInChildren<MeshRenderer>().material = _material;
            _thisObject = transform.parent;

            _sensorObject = new SensorObject() { Sensor = _thisObject.gameObject };
            switch (_thisObject.name)
            {
                case "ColorSensor(Clone)":
                    _sensorObject.Type = SensorTypes.ColorSensor;
                    break;
                case "IRSensor(Clone)":
                    _sensorObject.Type = SensorTypes.InfraredSensor;
                    break;
                case "TouchSensor(Clone)":
                    _sensorObject.Type = SensorTypes.TouchSensor;
                    break;
                case "UltrasonicSensor(Clone)":
                    _sensorObject.Type = SensorTypes.UltrasonicSensorCm;
                    break;
            }

            _material.color = _originColor;
            //_thisObject.position = new Vector3(100, _thisObject.position.y, 0);
            _minHeight = _thisObject.position.y;
            _rotationX = _thisObject.rotation.x;
            _rotationY = _thisObject.rotation.y;
            _rotationZ = _thisObject.rotation.z;
        }

        void OnMouseDown()
        {
            _choosen = !_choosen;
            if (_choosen)
            {
                _material.color = _choosenColor + _originColor;
                AddItemToField.ChoosenId = _id;
            }
            else
            {
                _material.color = _originColor;
                AddItemToField.ChoosenId = 0;
            }
        }

        void Update()
        {
            if (SensorData.SensorPorts[_port - 1] != _sensorObject)
                _port = 5;
            if (_choosen && AddItemToField.ChoosenId != _id)
            {
                _choosen = !_choosen;
                _material.color = _originColor;
            }
            if (!_choosen) return;
            if (Input.GetKeyDown(Delete))
            {
                Destroy(_thisObject.gameObject);
                if (_port != 5)
                {
                    SensorData.SensorPorts[_port - 1] = null;
                    GameObject.Find("SensorPorts").transform.GetChild(_port).GetComponent<Text>().text =
                        _port.ToString() + ":";
                }
                AddItemToField.ChoosenId = 0;
                return;
            }
            if (Input.GetKeyDown(One)) ChangePort(1);
            if (Input.GetKeyDown(Two)) ChangePort(2);
            if (Input.GetKeyDown(Three)) ChangePort(3);
            if (Input.GetKeyDown(Four)) ChangePort(4);

            if (!Input.GetKey(SwitchCode)) //перемещение
            {
                if (Input.GetKey(Left)) _x = -1;
                else if (Input.GetKey(Right)) _x = 1;
                else _x = 0;

                if (Input.GetKey(Back)) _y = -1;
                else if (Input.GetKey(Forward)) _y = 1;
                else _y = 0;

                if (Input.GetKey(Up))
                    if (_height < _maxHeight) _tmpHeight += 10;
                if (Input.GetKey(Down))
                    if (_height > _minHeight) _tmpHeight -= 10;
                _tmpHeight = Mathf.Clamp(_tmpHeight, _minHeight, _maxHeight);
                _height = Mathf.Lerp(_height, _tmpHeight, 3 * Time.deltaTime);
                //Vector3 direction = new Vector3(_x, _y, 0);
                //_thisObject.Translate(direction * SpeedMove * Time.deltaTime);
                _thisObject.position = new Vector3(_thisObject.position.x + _x * SpeedMove, _height, _thisObject.position.z + _y * SpeedMove);
            }
            else //вращение
            {
                if (Input.GetKey(Back)) _rotationZ -= SpeedRotate;
                else if (Input.GetKey(Forward)) _rotationZ += SpeedRotate;


                if (Input.GetKey(Left)) _rotationY -= SpeedRotate;
                else if (Input.GetKey(Right)) _rotationY += SpeedRotate;

                if (Input.GetKey(Up))
                    _rotationX += SpeedRotate;
                else if (Input.GetKey(Down))
                    _rotationX -= SpeedRotate;

                _thisObject.rotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);
            }
        }

        void ChangePort(int i)
        {
            if (i == _port) return;

            Text text = GameObject.Find("SensorPorts").transform.GetChild(i).GetComponent<Text>();
            if (_port != 5)
            {
                GameObject.Find("SensorPorts").transform.GetChild(_port).GetComponent<Text>().text =
                    _port.ToString() + ":";
                SensorData.SensorPorts[_port - 1] = null;
            }
            _port = i;
            SensorData.SensorPorts[i - 1] = _sensorObject;
            text.text = i.ToString() + ": ";
            switch (_sensorObject.Type)
            {
                case SensorTypes.ColorSensor:
                    text.text += "Датчик цвета";
                    break;
                case SensorTypes.InfraredSensor:
                    text.text += "Инфракрасный датчик";
                    break;
                case SensorTypes.TouchSensor:
                    text.text += "Датчик касания";
                    break;
                case SensorTypes.UltrasonicSensorCm:
                    text.text += "Ультразвуковой датчик";
                    break;
            }
        }
    }
}