using UnityEngine;

namespace Assets.Scripts.UnityScripts.TestScripts
{
    public class ButtonProccess : MonoBehaviour
    {
        private float distance = 1f;
        public Camera BtnCamera;


        void Update()
        {
            RaycastHit hit;
            if (Physics.Raycast(BtnCamera.transform.position, BtnCamera.transform.forward, out hit, distance))
            {
                
            }
        }
    }
}