using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class Save : MonoBehaviour
    {


        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            if (SensorData.Prefab == null) return;
            string path = EditorUtility.SaveFilePanel(
                "Сохранить сцену",
                "C:/Users/Рина/Documents/LegoVirtualRobot/Scenes",
                "NewScene",
                "prefab");
            if (path != null)
            {
                Type staticClass = typeof(SensorData);
                try
                {
                    FieldInfo[] fields = staticClass.GetFields(BindingFlags.Static | BindingFlags.Public);

                    object[,]
                        a = new object[fields.Length - 1, 2]; //one field can´t be serialized, so shouldn´t be counted
                    int i = 0;
                    foreach (FieldInfo field in fields)
                    {
                        if (field.Name == "Prefab")
                            continue;
                        a[i, 0] = field.Name;
                        a[i, 1] = field.GetValue(null);
                        i++;
                    }
                    Stream f = File.Open(path, FileMode.Create);
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(f, a);
                    f.Close();
                    string prefabPath = "Assets/Resources/SavingFields/" + path.Split('/')[path.Split('/').Length - 1];
                    UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab(prefabPath);
                    PrefabUtility.ReplacePrefab(SensorData.Prefab, prefab, ReplacePrefabOptions.ConnectToPrefab);

                }
                catch
                {
                   Debug.Log("Serialize is failed");
                }
            }
            BinaryFormatter binFormat = new BinaryFormatter(); 
        }
    }
}