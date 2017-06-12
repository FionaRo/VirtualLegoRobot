using System;
using System.Linq;
using Assets.Scripts.Data;

namespace Assets.Scripts.UnityScripts.TestScripts
{
    public class GetProccess
    {
        private static Variable _oldValue;
        private static bool _time;

        public static void Proccess()
        {
            Get query = GlobalVariables.ExportData.Peek();
            if (query.Timer != null)
            {
                if (!_time)
                {
                    SensorData.Timer[0] = 0;
                    _time = true;
                }
                if (SensorData.Timer[0] >= (float) Variable.Cast(query.Timer, "Single").Value)
                {
                    SensorData.Timer[0] = 0;
                    _time = false;
                    query.CanContinue = true;
                }
            }
            if (query.IsComparsion)
            {
                float a, b = (float)Variable.Cast(query.ValueCompare, "Single").Value;
                if (query.IsWait)
                {
                    switch (query.SensorType)
                    {
                        case SensorTypes.Buttons:
                            if (Compare((float[])Variable.Cast(query.Buttons, "Single[]").Value, (int)b,
                                ref query.ResultChange))
                                query.CanContinue = true;
                            break;
                        case SensorTypes.ColorSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (Compare(a, (float[])Variable.Cast(query.ArrayCompare, "Single[]").Value))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.ReflectedLight:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.InfraredSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.InfraredSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.LargeMotorDegrees:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.LargeMotorRotations:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.LargeMotorSpeed:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType3;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.Timer:
                            int t = (int)Variable.Cast(query.Timer, "Int32").Value;
                            if (t >= 1 && t <= 8)
                            {
                                a = SensorData.Timer[t];
                                if (Compare(a, b, query.Comparasion))
                                {
                                    query.CanContinue = true;
                                    query.ResultChange.Value = a;
                                }
                            }
                            break;
                        case SensorTypes.TouchSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.TouchSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.UltrasonicSensorCm:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                        case SensorTypes.UltrasonicSensorInches:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType2;
                            if (Compare(a, b, query.Comparasion))
                            {
                                query.CanContinue = true;
                                query.ResultChange.Value = a;
                            }
                            break;
                    }
                }
                else
                {
                    switch (query.SensorType)
                    {
                        case SensorTypes.Buttons:
                            query.ResultCompare.Value =
                                Compare((float[])Variable.Cast(query.Buttons, "Single[]").Value, (int)b,
                                    ref query.ResultChange);
                            break;
                        case SensorTypes.ColorSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            query.ResultCompare.Value =
                                Compare(a, (float[])Variable.Cast(query.ArrayCompare, "Single[]").Value);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.ReflectedLight:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.InfraredSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type !=
                                SensorTypes.InfraredSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.LargeMotorDegrees:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.MotorPorts[query.Port - 'A' - 1].ValueType1;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.LargeMotorRotations:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.MotorPorts[query.Port - 'A' - 1].ValueType2;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.LargeMotorSpeed:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type !=
                                SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - 'A' - 1].ValueType3;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.Timer:
                            int t = (int)Variable.Cast(query.Timer, "Int32").Value;
                            if (t >= 1 && t <= 8)
                            {
                                a = SensorData.Timer[t];
                                query.ResultCompare.Value = Compare(SensorData.Timer[t], b, query.Comparasion);
                                query.ResultChange.Value = a;
                            }
                            else
                            {
                                query.ResultCompare.Value = false;
                                query.ResultChange.Value = 0;
                            }
                            break;
                        case SensorTypes.TouchSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.TouchSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.UltrasonicSensorCm:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                        case SensorTypes.UltrasonicSensorInches:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type !=
                                SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0'].ValueType2;
                            query.ResultCompare.Value = Compare(a, b, query.Comparasion);
                            query.ResultChange.Value = a;
                            break;
                    }
                    query.CanContinue = true;
                    return;
                }
            }
            else
            {
                if (query.IsWait)
                {
                    float a;
                    switch (query.SensorType)
                    {
                        case SensorTypes.Buttons:
                            if (_oldValue == null)
                                _oldValue = new Variable { Value = SensorData.Buttons };
                            else if (Compare(SensorData.Buttons, _oldValue, ref query.ResultChange))
                            {
                                _oldValue = null;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.ColorSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.ReflectedLight:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.InfraredSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.InfraredSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.LargeMotorDegrees:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.MotorPorts[query.Port - 'A' - 1].ValueType1;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.LargeMotorRotations:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.MotorPorts[query.Port - 'A' - 1].ValueType2;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.LargeMotorSpeed:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.MotorPorts[query.Port - 'A' - 1].ValueType3;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.Timer:
                            int t = (int)Variable.Cast(query.Timer, "Int32").Value;
                            if (t >= 1 && t <= 8)
                            {
                                a = SensorData.Timer[t];
                                if (_oldValue == null)
                                    _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                                else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                                {
                                    _oldValue = null;
                                    query.ResultChange.Value = a;
                                    query.CanContinue = true;
                                }
                            }
                            break;
                        case SensorTypes.TouchSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.TouchSensor)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.UltrasonicSensorCm:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                        case SensorTypes.UltrasonicSensorInches:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            a = (float)SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            if (_oldValue == null)
                                _oldValue = new Variable() { Value = a, ValueType = typeof(float) };
                            else if (Compare(a, _oldValue, query.Comparasion, query.ValueCompare))
                            {
                                _oldValue = null;
                                query.ResultChange.Value = a;
                                query.CanContinue = true;
                            }
                            break;
                    }

                }
                else
                {
                    switch (query.SensorType)
                    {
                        case SensorTypes.Buttons:
                            query.ResultCompare.Value = SensorData.Buttons[0] != ButtonConditions.Realised;
                            break;
                        case SensorTypes.ColorSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            break;
                        case SensorTypes.ReflectedLight:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.ColorSensor)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.SensorPorts[query.Port - '0' - 1].ValueType2;
                            break;
                        case SensorTypes.InfraredSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0' - 1].Type != SensorTypes.InfraredSensor)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.SensorPorts[query.Port - '0' - 1].ValueType1;
                            break;
                        case SensorTypes.LargeMotorDegrees:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.MotorPorts[query.Port - 'A'].ValueType1;
                            break;
                        case SensorTypes.LargeMotorRotations:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.MotorPorts[query.Port - 'A'].ValueType2;
                            break;
                        case SensorTypes.LargeMotorSpeed:
                            if (query.Port == '0' || SensorData.MotorPorts[query.Port - 'A'].Type != SensorTypes.LargeMotorDegrees)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = SensorData.MotorPorts[query.Port - 'A'].ValueType3;
                            break;
                        case SensorTypes.Timer:
                            int t = (int)Variable.Cast(query.Timer, "Int32").Value;
                            if (t >= 1 && t <= 8)
                                query.ResultChange.Value = SensorData.Timer[t];
                            else
                                query.ResultChange.Value = 0;
                            break;
                        case SensorTypes.TouchSensor:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.TouchSensor)
                                throw new Exception("Wrong port");
                            query.ResultCompare.Value = (bool)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            break;
                        case SensorTypes.UltrasonicSensorCm:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = (bool)SensorData.SensorPorts[query.Port - '0'].ValueType1;
                            break;
                        case SensorTypes.UltrasonicSensorInches:
                            if (query.Port == '0' || SensorData.SensorPorts[query.Port - '0'].Type != SensorTypes.UltrasonicSensorCm)
                                throw new Exception("Wrong port");
                            query.ResultChange.Value = (bool)SensorData.SensorPorts[query.Port - '0'].ValueType2;
                            break;
                    }
                    query.CanContinue = true;
                    return;
                }
            }
        }


        private static bool Compare(float a, float b, ComparasionTypes compareType)
        {
            switch (compareType)
            {
                case ComparasionTypes.Bigger:
                    return a > b;
                case ComparasionTypes.NotEqual:
                    return Math.Abs(a - b) > 1e-3;
                case ComparasionTypes.BiggerOrEqual:
                    return a >= b;
                case ComparasionTypes.Equal:
                    return Math.Abs(a - b) < 1e-3;
                case ComparasionTypes.Smaller:
                    return a < b;
                case ComparasionTypes.SmallerOrEqual:
                    return a <= b;
                default:
                    throw new Exception("Unexpected comparasion type");
            }
        }

        private static bool Compare(float a, float[] array)
        {
            return array.Contains(a);
        }

        private static bool Compare(float[] btns, int b, ref Variable vOut)
        {
            foreach (var btn in btns)
            {
                if ((int)btn >= 0 && (int)btn <= 5)
                {
                    if ((int)SensorData.Buttons[(int)btn] == b)
                    {
                        vOut.Value = (int)btn;
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool Compare(float a, Variable b, ComparasionTypes compareType, Variable amount)
        {
            float y = (float)Variable.Cast(b, "Single").Value;
            switch (compareType)
            {
                case ComparasionTypes.Bigger:
                    return a >= y + (float)Variable.Cast(amount, "Single").Value;
                case ComparasionTypes.Smaller:
                    return a <= y - (float)Variable.Cast(amount, "Single").Value;
                case ComparasionTypes.NotEqual:
                    return Math.Abs(a - y) >= (float)Variable.Cast(amount, "Single").Value;
                default:
                    throw new Exception("Error in comparasion type");

            }
        }

        private static bool Compare(ButtonConditions[] a, Variable b, ref Variable btn)
        {
            ButtonConditions[] x = (ButtonConditions[])b.Value;

            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != a[i])
                {
                    btn.Value = i;
                    return true;
                }
            }
            return false;
        }

        private static bool Compare(float a, Variable b)
        {
            return Math.Abs(a - (float)b.Value) > 1e-3;
        }
    }
}