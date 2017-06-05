using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts
{
    public class CameraMove2 : MonoBehaviour
    {

        public float Speed = 5;

        public KeyCode Left = KeyCode.A;
        public KeyCode Right = KeyCode.D;
        public KeyCode Up = KeyCode.W;
        public KeyCode Down = KeyCode.S;
        public KeyCode RotCamLeft = KeyCode.E;
        public KeyCode RotCamRight = KeyCode.Q;
        public KeyCode RotCamDown = KeyCode.R;
        public KeyCode RotCamUp = KeyCode.T;

        public Transform StartPoint;
        public int RotationX = 70;
        public float MaxHeight = 100;
        public float MinHeight = 5;

        private float _camRotationY;
        private float _camRotationX;
        private float _height;
        private float _tmpHeight;
        private float _h, _v;

        void Start()
        {
            _tmpHeight = _height;
            transform.position = new Vector3(StartPoint.position.x, StartPoint.position.y, StartPoint.position.z);
        }

        void Update()
        {
            if (Input.GetKey(Left)) _h = -1; else if (Input.GetKey(Right)) _h = 1; else _h = 0;
            if (Input.GetKey(Down)) _v = -1; else if (Input.GetKey(Up)) _v = 1; else _v = 0;

            if (Input.GetKey(RotCamRight)) _camRotationY -= 3; else if (Input.GetKey(RotCamLeft)) _camRotationY += 3;

            if (Input.GetKey(RotCamDown)) _camRotationX -= 3; else if (Input.GetKey(RotCamUp)) _camRotationX += 3;

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (_height < MaxHeight) _tmpHeight += 10;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (_height > MinHeight) _tmpHeight -= 10;
            }

            _height = Mathf.Lerp(_height, _tmpHeight, 3 * Time.deltaTime);

            //Vector3 direction = new Vector3(_h, _v, 0);
            //transform.Translate(direction * Speed * 3 * Time.deltaTime);
            transform.position = new Vector3(transform.position.x + _h * Speed, _height, transform.position.z + _v * Speed);
            transform.rotation = Quaternion.Euler(_camRotationX, _camRotationY, 0);
        }
    }
}