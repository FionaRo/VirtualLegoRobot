using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.TestScripts
{
    public class ButtonProccess : MonoBehaviour
    {
        void Start()
        {
        }

        void OnEnable()
        {
            Debug.Log("Enabled");
        }

        void OnDisable()
        {
            Debug.Log("Disabled");
        }

    }
}