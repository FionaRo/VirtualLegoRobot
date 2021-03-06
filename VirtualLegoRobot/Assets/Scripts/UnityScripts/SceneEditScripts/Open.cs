﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Data;
using Assets.Scripts.ProgramScripts;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UnityScripts.SceneEditScripts
{
    public class Open : MonoBehaviour
    {
        private readonly Color _enabled = Color.gray;
        public Transform MotorLeft, MotorRight;
        public Transform Sensors;

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(TaskOnClick);
        }

        void TaskOnClick()
        {
            string path = EditorUtility.OpenFilePanel(
                "Загрузить сцену",
                "C:/Users/Рина/Documents/LegoVirtualRobot/Scenes",
                "prefab");
            if (path.Length != 0)
            {
                var curField = GameObject.Find("Field");
                foreach (GameObject child in curField.transform)
                    if (child != null)
                        Destroy(child);
                curField.transform.DetachChildren();
                Destroy(curField);

                string prefabPath = "SavingFields/" + path.Split('/')[path.Split('/').Length - 1].Split('.')[0];
                GameObject prefab = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
                Instantiate(prefab);
                SensorData.Prefab = GameObject.Find("Field");
                Type staticClass = typeof(SensorData);
                try
                {
                    FieldInfo[] fields = staticClass.GetFields(BindingFlags.Static | BindingFlags.Public);
                    object[,] a;
                    Stream f = File.Open(path, FileMode.Open);
                    BinaryFormatter formatter = new BinaryFormatter();
                    a = formatter.Deserialize(f) as object[,];
                    f.Close();
                    if (a == null || a.GetLength(0) != fields.Length - 1) throw new Exception();

                    foreach (FieldInfo field in fields)
                        for (int i = 0; i < fields.Length - 1; i++) 
                            if (field.Name == (a[i, 0] as string))
                                field.SetValue(null, a[i, 1]);
                    for (int i = 0; i < 4; i++)
                    {
                        SensorData.MotorPorts[i] = null; //TODO понять где левый, а где правый мотор
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        SensorData.SensorPorts[i] = null; //TODO вывод сообщений
                    }
                }
                catch
                {
                    Debug.Log("Deserialize is failed");
                }
            }
        }
    }
}