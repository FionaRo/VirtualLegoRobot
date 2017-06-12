using System.Threading;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.TestScripts
{
    public class RunThisProgram : MonoBehaviour
    {
        public static Thread Thread;

        void Start()
        {
            Button btn = GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            string programName = GetComponentInChildren<Text>().text;
            if (Thread != null)
                Thread.Interrupt();
            GlobalVariables.CurrentProgram.ProgramName = programName;
            Thread = new Thread(GlobalVariables.CurrentProgram.Start);
            Thread.Start();
        }
    }
}