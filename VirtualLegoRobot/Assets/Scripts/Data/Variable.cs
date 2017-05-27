using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public struct Variable
    {
        public object Value;
        public Type ValueType;

        public static Variable StringToNeededType(string value, string type)
        {
            Variable transformValue = new Variable();
            switch (type)
            {
                case "Single":
                    transformValue.Value = double.Parse(value);
                    transformValue.ValueType = typeof(double);
                    break;
                case "Int32":
                    transformValue.Value = int.Parse(value);
                    transformValue.ValueType = typeof(int);
                    break;
                case "Boolean":
                    transformValue.Value = bool.Parse(value);
                    transformValue.ValueType = typeof(bool);
                    break;
                case "String":
                    transformValue.Value = value;
                    transformValue.ValueType = typeof(string);
                    break;
                case "Single[]":
                    value = value.Substring(1, value.Length - 2);
                    string[] arrayStringForDouble = value.Split(',');
                    double[] arrayDouble = new double[value.Length];
                    for (int i = 0; i < arrayStringForDouble.Length; i++)
                        arrayDouble[i] = double.Parse(arrayStringForDouble[i]);
                    transformValue.Value = arrayDouble;
                    transformValue.ValueType = typeof(double[]);
                    break;
                case "Boolean[]":
                    value = value.Substring(1, value.Length - 2);
                    string[] arrayStringForBool = value.Split(',');
                    bool[] arrayBool = new bool[value.Length];
                    for (int i = 0; i < arrayStringForBool.Length; i++)
                        arrayBool[i] = bool.Parse(arrayStringForBool[i]);
                    transformValue.Value = arrayBool;
                    transformValue.ValueType = typeof(bool[]);
                    break;
                default:
                    throw new Exception("Unexpected type StringToNeededType");
            }
            return transformValue;
        }

        public static Variable WireToNeededType(string wire, string type)
        {
            return new Variable();
        }
    }
}
