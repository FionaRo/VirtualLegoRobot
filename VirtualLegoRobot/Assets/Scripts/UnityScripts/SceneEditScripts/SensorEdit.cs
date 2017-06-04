using System;
using UnityEngine;

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

        private Transform _thisObject;
        private float _maxHeight = 100f;
        private float _minHeight;
        private float _x, _y;
        private float _height;
        private float _tmpHeight;
        private float _rotationX;
        private float _rotationY;
        private float _rotationZ;

        void Start()
        {

            _material = new Material(Shader.Find("Specular"));
            GetComponentInChildren<MeshRenderer>().material = _material;
            _thisObject = transform.parent;
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
            _material.color = _choosen ? _originColor + _choosenColor : _originColor;
            Debug.Log("Choosen");
        }

        void Update()
        {
            if (!_choosen) return;
            if (Input.GetKeyDown(Delete))
            {
                Destroy(_thisObject.gameObject);
                return;
            }
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
    }
}