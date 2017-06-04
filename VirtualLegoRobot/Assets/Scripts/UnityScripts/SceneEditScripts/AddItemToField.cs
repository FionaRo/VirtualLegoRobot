using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class AddItemToField : MonoBehaviour
    {
        public Button Button;
        public Transform Prefab;
        public Transform Parent;
        public float Y;

        void Start()
        {
            Button btn = Button.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            var cams = Prefab.GetComponentsInChildren<Camera>();
            if (cams.Length != 0)
                foreach (var cam in cams)
                {
                    cam.enabled = false;
                }
            Instantiate(Prefab);
        }
    }
}