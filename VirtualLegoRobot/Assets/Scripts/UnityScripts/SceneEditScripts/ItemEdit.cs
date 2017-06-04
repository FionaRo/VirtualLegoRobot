using UnityEngine;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class ItemEdit : MonoBehaviour
    {
        private bool _choosen;
        private readonly Color[] _setOfColors =
             {Color.white, Color.black, Color.blue, Color.green, Color.yellow, Color.red};
        private readonly Color _choosenColor = Color.cyan;
        private int _curColor;
        private Material _material;
        private readonly Vector3 _speedChangingSize = new Vector3(0.1f, 0.1f, 0.1f);
        private Transform _thisObject;

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
        public KeyCode IncSize = KeyCode.Equals;
        public KeyCode DecSize = KeyCode.Minus;

        private float _x, _z;
        private float _rotationX;
        private float _rotationY;
        private float _rotationZ;

        private void Start()
        {
            _material = new Material(Shader.Find("Specular"));
            GetComponentInChildren<MeshRenderer>().material = _material;
            _material.color = _setOfColors[_curColor];
            _thisObject = GetComponentInParent<Transform>();
            _rotationX = _thisObject.rotation.x;
            _rotationY = _thisObject.rotation.y;
            _rotationZ = _thisObject.rotation.z;
        }

        void OnMouseDown()
        {
            _choosen = !_choosen;
            _material.color = _choosen ? _choosenColor + _setOfColors[_curColor] : _setOfColors[_curColor];
        }

        void Update()
        {
            if (!_choosen) return;
            if (Input.GetKeyDown(Delete))
            {
                Destroy(_thisObject.gameObject);
                return;
            }

            if (Input.GetKey(IncSize))
                _thisObject.localScale += _speedChangingSize;
            if (Input.GetKey(DecSize))
                _thisObject.localScale -= _speedChangingSize;
            if (Input.GetMouseButtonDown(2))
            {
                _curColor = ++_curColor % _setOfColors.Length;
                _material.color = _setOfColors[_curColor] + (_choosen ? _choosenColor : new Color(0, 0, 0));
            }
            if (!Input.GetKey(SwitchCode)) //перемещение
            {
                if (Input.GetKey(Left)) _x = -1;
                else if (Input.GetKey(Right)) _x = 1;
                else _x = 0;

                if (Input.GetKey(Back)) _z = -1;
                else if (Input.GetKey(Forward)) _z = 1;
                else _z = 0;

                Vector3 direction = new Vector3(_x, _z, 0);
                //_thisObject.Translate(direction * SpeedMove * Time.deltaTime);
                _thisObject.position = new Vector3(_thisObject.position.x + _x * SpeedMove, _thisObject.position.y, _thisObject.position.z + _z * SpeedMove);
            }
            else //вращение
            {
                if (Input.GetKeyDown(Back)) _rotationZ -= 90;
                else if (Input.GetKeyDown(Forward)) _rotationZ += 90;


                if (Input.GetKey(Left)) _rotationY -= SpeedRotate;
                else if (Input.GetKey(Right)) _rotationY += SpeedRotate;

                if (Input.GetKeyDown(Up))
                    _rotationX += 90;
                else if (Input.GetKeyDown(Down))
                    _rotationX -= 90;

                _thisObject.rotation = Quaternion.Euler(_rotationX, _rotationY, _rotationZ);
            }
        }
    }
}