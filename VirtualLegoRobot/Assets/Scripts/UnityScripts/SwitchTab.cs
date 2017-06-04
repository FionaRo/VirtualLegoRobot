using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts
{
    public class SwitchTab : MonoBehaviour
    {
        public Button Button;


        void Start()
        {
            Button btn = Button.GetComponent<Button>();
            btn.onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            string tabName = Button.GetComponentInChildren<Text>().text;
            switch (tabName)
            {
                case "Тест":

                    if (SceneManager.GetActiveScene().buildIndex != 0)
                    {
                        SceneManager.LoadScene(0);

                        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
                    }
                    break;
                case "Поле":
                    if (SceneManager.GetActiveScene().buildIndex != 2)
                    {
                        SceneManager.LoadScene(2);
                        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
                    }
                    break;
                case "Робот":
                    if (SceneManager.GetActiveScene().buildIndex != 1)
                    {
                        SceneManager.LoadScene(1);
                        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
                    }
                    break;
            }
        }
    }
}