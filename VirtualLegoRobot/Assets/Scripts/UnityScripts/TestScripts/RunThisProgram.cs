using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Data;

public class RunThisProgram : MonoBehaviour
{
    public Button Button;
    public static Thread Thread = null;

    void Start()
    {
        Button btn = Button.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        string programName = Button.GetComponentInChildren<Text>().text;
        if (Thread != null)
        {
            Thread.Interrupt();
        }
        GlobalVariables.CurrentProgram.ProgramName = programName;
        Thread = new Thread(GlobalVariables.CurrentProgram.Start);
        Thread.Start();
    }
}