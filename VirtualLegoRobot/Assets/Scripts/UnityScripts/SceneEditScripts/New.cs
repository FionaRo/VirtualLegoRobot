using Assets.Scripts.Data;
using Assets.Scripts.ProgramScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class New : MonoBehaviour
    {
        public GameObject Prefab;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            var field = GameObject.Find("Field");
            foreach (GameObject child in field.transform)
                if (child != null)
                    Destroy(child);
            field.transform.DetachChildren();
            Destroy(field);
            var go = new GameObject("Field");
            Instantiate(Prefab, go.transform);
            SensorData.Prefab = go;
        }
    }
}