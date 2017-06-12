using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    public class Variable
    {
        public static Dictionary<string, Type> VarTypes = new Dictionary<string, Type>()
        {
            {"Single", typeof(float)},
            {"Int32", typeof(int)},
            {"Boolean", typeof(bool)},
            {"String", typeof(string)},
            {"Single[]", typeof(float[])},
            {"Boolean[]", typeof(bool[])}
        };

        public object Value;
        public Type ValueType;

        public static Variable StringToNeededType(string value, string type)
        {
            Variable transformValue = new Variable() {ValueType = VarTypes[type]};
            
            switch (type)
            {
                case "Single":
                    transformValue.Value = float.Parse(value);
                    break;
                case "Int32":
                    transformValue.Value = int.Parse(value);
                    break;
                case "Boolean":
                    transformValue.Value = bool.Parse(value);
                    break;
                case "String":
                    transformValue.Value = value;
                    break;
                case "Single[]":
                    value = value.Substring(1, value.Length - 2);
                    string[] arrayStringForfloat = value.Split(',');
                    float[] arrayfloat = new float[value.Length];
                    for (int i = 0; i < arrayStringForfloat.Length; i++)
                        arrayfloat[i] = float.Parse(arrayStringForfloat[i]);
                    transformValue.Value = arrayfloat;
                    break;
                case "Boolean[]":
                    value = value.Substring(1, value.Length - 2);
                    string[] arrayStringForBool = value.Split(',');
                    bool[] arrayBool = new bool[value.Length];
                    for (int i = 0; i < arrayStringForBool.Length; i++)
                        arrayBool[i] = bool.Parse(arrayStringForBool[i]);
                    transformValue.Value = arrayBool;
                    break;
                default:
                    throw new Exception("Unexpected type StringToNeededType");
            }
            return transformValue;
        }

        public static Variable WireToNeededType(string wire, string type)
        {
            return Cast(Variables.ValueFromWires[wire], type);
        }

        public static Variable Cast(Variable var, string type)
        {
            if (var.ValueType == VarTypes[type]) return var;
            Variable newVar = new Variable() { ValueType = VarTypes[type] };
            switch (type)
            {
                case "Single":
                case "Int32":
                    if (newVar.ValueType == VarTypes["Boolean"])
                    {
                        newVar.Value = ((bool)var.Value ? 1 : 0);
                        return newVar;
                    }
                    return var;
                case "String":
                    newVar.Value = var.Value.ToString();
                    return newVar;
                case "Single[]":
                    if (newVar.ValueType == VarTypes["Single"] || newVar.ValueType == VarTypes["Int32"])
                    {
                        float[] arr = { (float)var.Value };
                        newVar.Value = arr;
                        return newVar;
                    }
                    return var;
                case "Boolean[]":
                    if (newVar.ValueType == VarTypes["Boolean"])
                    {
                        bool[] arr = { (bool)var.Value };
                        newVar.Value = arr;
                        return newVar;
                    }
                    return var;
                default:
                    throw new Exception("Unexpected type StringToNeededType");
            }
        }

    }
}
