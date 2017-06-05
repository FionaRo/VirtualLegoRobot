using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class CarEdit : MonoBehaviour
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

        private float _x, _y;
        private float _rotationX;
        private float _rotationY;
        private float _rotationZ;
        private int _id;
        private float _height;

        void Start()
        {
            _id = ++AddItemToField.Count;
            //_material = new Material(Shader.Find("Specular"));
            //GetComponentInChildren<MeshRenderer>().material = _material;
            _thisObject = transform;

            //_material.color = _originColor;
            //_thisObject.position = new Vector3(100, _thisObject.position.y, 0);
            _height = _thisObject.position.y;
            _rotationX = _thisObject.rotation.x;
            _rotationY = _thisObject.rotation.y;
            _rotationZ = _thisObject.rotation.z;
        }

        void OnMouseDown()
        {
            _choosen = !_choosen;
            if (_choosen)
            {
                //_material.color = _choosenColor + _originColor;
                AddItemToField.ChoosenId = _id;
            }
            else
            {
                //_material.color = _originColor;
                AddItemToField.ChoosenId = 0;
            }
            Debug.Log("Ok");
        }

        void Update()
        {
            if (_choosen && AddItemToField.ChoosenId != _id)
            {
                _choosen = !_choosen;
                //_material.color = _originColor;
            }
            if (!_choosen) return;
            if (Input.GetKeyDown(Delete))
            {
                Destroy(_thisObject.gameObject);
                AddItemToField.ChoosenId = 0;
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
                //Vector3 direction = new Vector3(_x, _y, 0);
                //_thisObject.Translate(direction * SpeedMove * Time.deltaTime);
                _thisObject.position = new Vector3(_thisObject.position.x + _x * SpeedMove, _thisObject.position.y, _thisObject.position.z + _y * SpeedMove);
            }
            else //вращение
            {
                if (Input.GetKey(Left)) _rotationY -= SpeedRotate;
                else if (Input.GetKey(Right)) _rotationY += SpeedRotate;
                _thisObject.rotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);
            }
        }
    }
}