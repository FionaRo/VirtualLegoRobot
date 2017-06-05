using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.ProgramScripts;
using Assets.Scripts.Data;

namespace Assets.Scripts.UnityScripts
{
    public class OpenFileDialog : MonoBehaviour
    {

        public Transform Prefab;
        public Transform Parent;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            string path = EditorUtility.OpenFilePanel(
                "Открыть новый проект",
                "",
                "ev3");
            if (path.Length != 0)
            {
                RunProgram run = new RunProgram(path);
                GlobalVariables.CurrentProgram = run;
                int i = 0;
                foreach (Transform child in Parent)
                {
                    Destroy(child.gameObject);
                }
                Parent.DetachChildren();
                foreach (var prog in run.RunningProgram)
                {
                    Instantiate(Prefab, Parent);
                    Parent.GetChild(i).GetChild(0).GetComponent<Text>().text = prog.Key;
                    i++;
                }
            }
        }
    }
}