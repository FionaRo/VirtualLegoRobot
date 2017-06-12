using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class ChangeMotorPort : MonoBehaviour
    {
        public Transform Panel;
        public SensorObject Motor;

        private readonly Color _enabled = Color.gray;
        private readonly Color _disabled = Color.white;
        private Image _image;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
            _image = GetComponent<Image>();
            _image.color = _disabled;
        }

        void TaskOnClick()
        {
            if (Motor == null) return;
            int choosenBtn = transform.GetComponentInChildren<Text>().text[0] - 'A';
            if (SensorData.MotorPorts[choosenBtn] != null) return;
            foreach (Transform child in Panel)
                child.GetComponent<Image>().color = _disabled;
            _image.color = _enabled;
            for (int i = 0; i < 4; i++)
            {
                if (SensorData.MotorPorts[i] == Motor)
                {
                    SensorData.MotorPorts[i] = null;
                    break;
                }
            }
            SensorData.MotorPorts[choosenBtn] = Motor;
        }
    }
}