using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public class Maths
    {
        public static Variable MathOperation(
            string blockName,
            List<Variable> valueIn,
            Variable chanhingValue,
            Variable index,
            Variable decimals,
            Variable lBound,
            Variable uBound,
            Variable percent,
            Variable basePower,
            Variable exponent)
        {
            Variable varOut = new Variable();
            //Работа с массивом
            if (chanhingValue != null)
            {
                if (index != null)
                {
                    if (valueIn != null)
                    {
                        varOut.Value = chanhingValue.Value;
                        varOut.ValueType = chanhingValue.ValueType;
                        if (varOut.ValueType == typeof(bool[]))
                            ((bool[])(varOut.Value))[(int)Variable.Cast(index, "Int32").Value] =
                                (bool)valueIn[0].Value;
                        else
                            ((float[])(varOut.Value))[(int)Variable.Cast(index, "Int32").Value] =
                                (float)Variable.Cast(valueIn[0], "Single").Value;
                    }
                    else
                    {
                        if (chanhingValue.ValueType == typeof(bool[]))
                        {
                            varOut.ValueType = typeof(bool);
                            varOut.Value = ((bool[])varOut.Value)[(int)Variable.Cast(index, "Int32").Value];
                        }
                        else
                        {
                            varOut.ValueType = typeof(float);
                            varOut.Value = ((float[])varOut.Value)[(int)Variable.Cast(index, "Int32").Value];
                        }
                    }
                }
                else if (valueIn != null)
                {
                    varOut.ValueType = chanhingValue.ValueType;
                    if (varOut.ValueType == typeof(bool[]))
                    {
                        int size = ((bool[])varOut.Value).Length;
                        bool[] newArray = (bool[])varOut.Value;
                        Array.Resize(ref newArray, size + 1);
                        newArray[size] = (bool)valueIn[0].Value;
                        varOut.Value = newArray;
                    }
                    else
                    {
                        int size = ((float[])varOut.Value).Length;
                        float[] newArray = (float[])varOut.Value;
                        Array.Resize(ref newArray, size + 1);
                        newArray[size] = (float)Variable.Cast(valueIn[0], "Single").Value;
                        varOut.Value = newArray;
                    }
                }
                else
                {
                    varOut.ValueType = typeof(int);
                    if (chanhingValue.ValueType == typeof(bool[]))
                        varOut.Value = ((bool[])varOut.Value).Length;
                    else
                        varOut.Value = ((float[])varOut.Value).Length;
                }
            }

            //truncate
            else if (decimals != null)
            {
                varOut.ValueType = valueIn[0].ValueType;
                float number = (float)Variable.Cast(valueIn[0], "Single").Value;
                int accuracy = (int)Variable.Cast(valueIn[1], "Int32").Value;
                float trunc = (float)Math.Round(number - number % (float)(Math.Pow(0.1, accuracy)), accuracy);
                varOut.Value = trunc;
            }

            else if (lBound != null)
            {
                if (valueIn == null) //random
                {
                    varOut.ValueType = typeof(int);
                    var rand = new Random();
                    varOut.Value = rand.Next((int)Variable.Cast(lBound, "Int32").Value,
                        (int)Variable.Cast(uBound, "Int32").Value);
                }
                else //comparasion
                {
                    float a = (float)Variable.Cast(lBound, "Single").Value;
                    float b = (float)Variable.Cast(uBound, "Single").Value;
                    float c = (float)Variable.Cast(valueIn[0], "Single").Value;
                    varOut.ValueType = typeof(bool);
                    if (blockName.Contains("Inside"))
                        varOut.Value = (a < c) && (c < b);
                    else
                        varOut.Value = (c < a) || (c > b);
                }
            }
            else if (percent != null)
            {
                var rand = new Random();
                varOut.ValueType = typeof(bool);
                varOut.Value = rand.Next(100) <= (int)Variable.Cast(percent, "Int32").Value;
            }
            //степень
            else if (basePower != null)
            {
                varOut.ValueType = typeof(float);
                varOut.Value = Math.Pow((float)Variable.Cast(basePower, "Single").Value,
                    (float)Variable.Cast(exponent, "Single").Value);
            }

            else
            {
                if (blockName.Contains("Boolean"))
                    varOut = BooleanOperation(blockName, valueIn);
                if (blockName.Contains("Arithmetic"))
                    varOut = ArithmeticOperation(blockName, valueIn);
                if (blockName.Contains("Round"))
                    varOut = RoundOperation(blockName, valueIn);
                if (blockName.Contains("Comparison"))
                    varOut = CompareOperation(blockName, valueIn);
                if (blockName.Contains("Concatenate"))
                    varOut = ConcatenateOperation(valueIn);
            }
            return varOut;
        }

        public static Variable BooleanOperation(string blockName, List<Variable> valueIn)
        {
            Variable varOut = new Variable() { ValueType = typeof(bool) };
            if (blockName.Contains("And"))
                varOut.Value = (bool)valueIn[0].Value && (bool)valueIn[1].Value;
            else if (blockName.Contains("Or"))
                varOut.Value = (bool)valueIn[0].Value || (bool)valueIn[1].Value;
            else if (blockName.Contains("XOr"))
                varOut.Value = (bool)valueIn[0].Value ^ (bool)valueIn[1].Value;
            else if (blockName.Contains("Not"))
                varOut.Value = !(bool)valueIn[0].Value;
            else
                throw new Exception("Unexpected boolean operation");
            return varOut;
        }

        public static Variable ArithmeticOperation(string blockName, List<Variable> valueIn)
        {
            Variable varOut = new Variable() { ValueType = typeof(float) };
            if (blockName.Contains("Add"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value +
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Subtract"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value -
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Divide"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value /
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Multiply"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value *
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Absolute"))
                varOut.Value = Math.Abs((float)Variable.Cast(valueIn[0], "Single").Value);
            else if (blockName.Contains("Square"))
                varOut.Value = Math.Sqrt((float)Variable.Cast(valueIn[0], "Single").Value);
            else
                throw new Exception("Unexpected arithmetic operation");
            return varOut;
        }

        public static Variable RoundOperation(string blockName, List<Variable> valueIn)
        {
            Variable varOut = new Variable() { ValueType = typeof(float) };
            if (blockName.Contains("Nearest"))
                varOut.Value = Math.Round((float)Variable.Cast(valueIn[0], "Single").Value);
            if (blockName.Contains("Up"))
                varOut.Value = Math.Ceiling((float)Variable.Cast(valueIn[0], "Single").Value);
            if (blockName.Contains("Down"))
                varOut.Value = Math.Floor((float)Variable.Cast(valueIn[0], "Single").Value);
            return varOut;
        }

        public static Variable CompareOperation(string blockName, List<Variable> valueIn)
        {
            Variable varOut = new Variable() { ValueType = typeof(bool) };
            if (blockName.Contains("GreaterEqual"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value >=
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("LessEqual"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value <=
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Greater"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value >
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("Less"))
                varOut.Value = (float)Variable.Cast(valueIn[0], "Single").Value <
                               (float)Variable.Cast(valueIn[1], "Single").Value;
            else if (blockName.Contains("NotEqual"))
                varOut.Value = Math.Abs((float)Variable.Cast(valueIn[0], "Single").Value - (float)Variable.Cast(valueIn[1], "Single").Value) > 1e-6;
            if (blockName.Contains("Equal"))
                varOut.Value = Math.Abs((float)Variable.Cast(valueIn[0], "Single").Value - (float)Variable.Cast(valueIn[1], "Single").Value) < 1e-6;
            return varOut;
        }

        public static Variable ConcatenateOperation(List<Variable> valueIn)
        {
            StringBuilder newStr = new StringBuilder();
            foreach (var subStr in valueIn)
                newStr.Append((string)Variable.Cast(subStr, "String").Value);
            return new Variable() { Value = newStr, ValueType = typeof(string) };
        }
    }
}