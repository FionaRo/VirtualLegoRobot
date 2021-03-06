﻿using EV3PDeserializeLib;
using EV3PDeserializeLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.ProgramScripts
{
    public class RunProgram
    {
        public Dictionary<string, DeserializedProgram> RunningProgram;
        public string ProgramName;

        public RunProgram(string path)
        {
            string extractPath = Unarchiver.EV3Extract(path);
            RunningProgram = new Dictionary<string, DeserializedProgram>();
            if (extractPath != null)
            {
                string[] programFiles = Directory.GetFiles(extractPath, "*.ev3p");
                foreach (string programName in programFiles)
                {
                    //xmlns мешается
                    string textFile;
                    using (StreamReader reader = new StreamReader(programName))
                    {
                        textFile = reader.ReadToEnd();
                        textFile = textFile.Replace("xmlns=", "notlink=");
                    }
                    using (StreamWriter writer = new StreamWriter(programName, false))
                    {
                        writer.Write(textFile);
                    }
                    string[] name = programName.Split('\\');
                    RunningProgram[name[name.Length - 1]] = SourceFile.Deserialize(programName);
                }
            }
            else
            {
                throw new Exception("File cannot be opened");
                //TODO вывод ошибки при распаковке файла
            }
        }

        public void Start()
        {
            Debug.Log(ProgramName);
            Run(RunningProgram[ProgramName]);
        }

        private void Run(DeserializedProgram program)
        {
            foreach (Wire wire in program.TurnRunning)
            {
                if (program.WiresDictionary[wire.Id] is PairedConfigurableMethodCall)
                {
                    var PCMCblock = (PairedConfigurableMethodCall)program.WiresDictionary[wire.Id];
                    Process(PCMCblock, program.Switch[PCMCblock.PairedStructure]);
                }
                else
                {
                    Process(program.WiresDictionary[wire.Id]);
                }
                if (Variables.Interrupt != null && Variables.LoopNames[Variables.Interrupt] > 0)
                    return;
            }
        }

        private void Process(IBlock block, ConfigurableFlatCaseStructure cases = null)
        {
            if (block is StartBlock)
            {
                SetMessage.SetStartMessage();
                return;
            }
            if (block is ConfigurableMethodCall)
            {
                ConfigurableMethodCall CMCblock = block as ConfigurableMethodCall;
                BlockTypes blockType = GetProgramType.GetBlockType(CMCblock.Target);

                switch (blockType)
                {
                    case BlockTypes.Data:
                        ProccessData(CMCblock);
                        break;
                    case BlockTypes.Display:
                        ProccessDisplay(CMCblock);
                        break;
                    case BlockTypes.Interrupt:
                        ProccessInterrupt(CMCblock);
                        if (Variables.Interrupt != null && Variables.LoopNames[Variables.Interrupt] > 0)
                            return;
                        break;
                    case BlockTypes.LargeMotor:
                        ProccessMotor(CMCblock);
                        break;
                    case BlockTypes.Personal:
                        ProccessPersonalBlock(CMCblock);
                        break;
                    case BlockTypes.Sensor:
                        ProccessSensors(CMCblock);
                        break;
                    case BlockTypes.Indicator:
                        ProccessIndicators(CMCblock);
                        break;
                }
                return;
            }
            if (block is ConfigurableWaitFor)
            {
                ProccessWait((ConfigurableWaitFor)block);
                return;
            }
            if (block is ConfigurableWhileLoop)
            {
                ProccessLoop((ConfigurableWhileLoop)block);
            }
            if (block is PairedConfigurableMethodCall)
            {
                ProccessSwitch((PairedConfigurableMethodCall)block, cases);
            }
        }

        void ProccessMotor(ConfigurableMethodCall motorBlock)
        {
            Debug.Log("Ok");
            string ports = null, unitDistance = null;
            float? distance = null, speed1 = null, speed2 = null, steering = null;
            bool isMove = false;
            foreach (var data in motorBlock.ConfigurableMethodTerminalList)
            {
                switch (data.Terminal.Id)
                {
                    case "MotorPort":
                    case "Ports":
                        ports = data.ConfiguredValue;
                        break;
                    case "Seconds":
                    case "Degrees":
                    case "Rotations":
                        unitDistance = data.Terminal.Id;
                        if (data.Terminal.Wire != null)
                            distance = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            distance = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Speed":
                    case "Speed\\ Left":
                        if (data.Terminal.Wire != null)
                            speed1 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            speed1 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        if (speed1 > 100)
                            speed1 = 100;
                        else if (speed1 < -100)
                            speed1 = -100;
                        break;
                    case "Speed\\ Right":
                        if (data.Terminal.Wire != null)
                            speed2 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            speed2 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        if (speed2 > 100)
                            speed2 = 100;
                        else if (speed2 < -100)
                            speed2 = -100;
                        break;
                    case "Steering":
                        if (data.Terminal.Wire != null)
                            steering = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            steering = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        if (steering > 100)
                            steering = 100;
                        else if (steering < -100)
                            steering = -100;
                        isMove = true;
                        break;
                    default:
                        throw new Exception("Error in proccessing motor");
                }
            }
            if (isMove) // блок рулевого управления преобразовываем к блоку независимого
            {
                if (steering >= 0)
                {
                    speed2 = ((50 - steering) / 50) * speed1;
                }
                else
                {
                    speed2 = speed1;
                    speed1 = ((50 - Math.Abs(steering.Value)) / 50) * speed2;
                }
            }
            SetMessage.SetMessageMotor(ports, isMove, speed1.Value, speed2.Value, steering.Value, distance.Value, unitDistance);
            Action.NewSetMotor(ports, speed1, speed2, distance, unitDistance);

            //нужен get если расстояние не 0
            if (unitDistance == null) return;
            char port;
            port = Math.Abs(speed1.Value) > Math.Abs(speed2.Value) ? ports[2] : ports[4];

            Variable dist = new Variable() { Value = distance, ValueType = typeof(float) };

            switch (unitDistance)
            {
                case "Seconds":
                    Action.NewGet(true, SensorTypes.Timer, true, port, ComparasionTypes.BiggerOrEqual, dist);
                    break;
                case "Rotations":
                    Action.NewGet(true, SensorTypes.LargeMotorRotations, true, port, ComparasionTypes.BiggerOrEqual, dist);
                    break;
                case "Degrees":
                    Action.NewGet(true, SensorTypes.LargeMotorDegrees, true, port, ComparasionTypes.BiggerOrEqual, dist);
                    break;
                default:
                    throw new Exception("Unexpected unit distance");
            }

            //TODO сон и оповещение 
            while (!GlobalVariables.ExportData.Peek().CanContinue) { }
            GlobalVariables.ExportData.Pop();
        }

        void ProccessDisplay(ConfigurableMethodCall displayBlock)
        {
            float x1 = 0, x2 = 0, y1 = 0, y2 = 0, radius = 0;
            bool clearScreen = true, invert = false, fill = false;
            string text = null;
            int size = 0;
            foreach (var data in displayBlock.ConfigurableMethodTerminalList)
            {
                switch (data.Terminal.Id)
                {
                    case "Text":
                        if (data.Terminal.Wire != null)
                            text = (string)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            text = data.ConfiguredValue;
                        break;
                    case "Clear\\ Screen":
                        if (data.Terminal.Wire != null)
                            clearScreen = (bool)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            clearScreen = (bool)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "X":
                    case "X1":
                    case "Column":
                        if (data.Terminal.Wire != null)
                            x1 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            x1 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Y":
                    case "Y1":
                    case "Row":
                        if (data.Terminal.Wire != null)
                            y1 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            y1 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Invert\\ Color":
                        if (data.Terminal.Wire != null)
                            invert = (bool)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            invert = (bool)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Size":
                        if (data.Terminal.Wire != null)
                            size = (int)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            size = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "X2":
                        if (data.Terminal.Wire != null)
                            x2 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            x2 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Y2":
                        if (data.Terminal.Wire != null)
                            y2 = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            y2 = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Radius":
                        if (data.Terminal.Wire != null)
                            radius = (float)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            radius = (float)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Fill":
                        if (data.Terminal.Wire != null)
                            fill = (bool)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            fill = (bool)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    default:
                        throw new Exception("Unsupported block");
                }
            }
            SetMessage.SetMessageDisplay();
            Action.NewSetDisplay(GetProgramType.GetDisplayType(displayBlock.Target), x1, y1, x2, y2, clearScreen, text, invert, size, radius, fill);
        }

        void ProccessIndicators(ConfigurableMethodCall indicatorBlock)
        {
            //LedOff==LedReset
            bool impulse = false;
            int color = 3;
            foreach (var data in indicatorBlock.ConfigurableMethodTerminalList)
            {
                switch (data.Terminal.Id)
                {
                    case "Color":
                        if (data.Terminal.Wire != null)
                            color = (int)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            color = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    case "Pulse":
                        if (data.Terminal.Wire != null)
                            impulse = (bool)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                        else
                            impulse = (bool)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                }
            }
            SetMessage.SetMessageIndicator();
            Action.NewSetIndicator(color, impulse);
        }

        void ProccessWait(ConfigurableWaitFor waitBlock)
        {
            if (waitBlock.Target.Contains("Compare"))
            {
                SensorTypes sensorType = GetProgramType.GetSensorType(waitBlock.Target);
                string wireResult = null, port = null;
                int comparasion = 2;
                Variable timer = null;
                Variable valueCompare = null;
                Variable arrayCompare = null, buttons = null;
                foreach (var data in waitBlock.ConfigurablemethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "MotorPort":
                        case "Port":
                            port = data.ConfiguredValue;
                            break;
                        case "Buttons":
                            if (data.Terminal.Wire != null)
                                buttons = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                buttons = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Timer":
                            if (data.Terminal.Wire != null)
                                timer = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                timer = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Comparasion":
                            if (data.Terminal.Wire != null)
                                comparasion = (int)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                            else
                                comparasion = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                            break;
                        case "Action":
                        case "ThresholdDegrees":
                        case "ThresholdRotations":
                        case "ThresholdSpeed":
                        case "Threshold":
                        case "How\\ Long":
                        case "Pressed\\,\\ Released\\ or\\ Bumped":
                            if (data.Terminal.Wire != null)
                                valueCompare = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                valueCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Set\\ of\\ colors":
                            if (data.Terminal.Wire != null)
                                arrayCompare = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                arrayCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Value":
                        case "Degrees":
                        case "Rotations":
                        case "Speed":
                        case "Timer\\ Value":
                        case "Color":
                        case "Proximity":
                        case "Distance":
                        case "DistanceInches":
                            if (data.Terminal.Wire != null)
                                wireResult = data.Terminal.Wire;
                            break;
                    }
                }
                SetMessage.SetMessageWait();
                Action.NewGet(true, sensorType, true, port == null ? '0' : port[2], (ComparasionTypes)comparasion, valueCompare, arrayCompare, buttons, timer);
                while (!GlobalVariables.ExportData.Peek().CanContinue)
                {
                }
                if (wireResult != null)
                {
                    float result = (float)GlobalVariables.ExportData.Peek().ResultChange.Value;
                    Variable variable = new Variable
                    {
                        Value = result,
                        ValueType = typeof(int)
                    };
                    Variables.ValueFromWires[wireResult] = variable;
                    GlobalVariables.ExportData.Pop();
                }
            }
            else
            {
                SensorTypes sensorType = GetProgramType.GetSensorType(waitBlock.Target);
                string wireResult = null, port = null;
                int comparasion = 1;
                Variable timer = null; ;
                Variable amount = null;
                foreach (var data in waitBlock.ConfigurablemethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "Timer":
                            if (data.Terminal.Wire != null)
                                timer = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                timer = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "MotorPort":
                        case "Port":
                            port = data.ConfiguredValue;
                            break;
                        case "Direction":
                            if (data.Terminal.Wire != null)
                                comparasion = (int)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                            else
                                comparasion = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                            break;
                        case "Amount":
                        case "Change":
                            if (data.Terminal.Wire != null)
                                amount = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                amount = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Degrees":
                        case "Rotations":
                        case "Speed":
                        case "Buttons":
                        case "Value":
                        case "Timer\\ Value":
                        case "Color":
                        case "Proximity":
                        case "State":
                        case "Distance":
                        case "DistanceInches":
                            if (data.Terminal.Wire != null)
                                wireResult = data.Terminal.Wire;
                            break;
                    }
                }
                SetMessage.SetMessageWait();
                Action.NewGet(true, sensorType, false, port == null ? '0' : port[2], (ComparasionTypes)comparasion, amount, timer: timer);
                //TODO сон и оповещение
                while (!GlobalVariables.ExportData.Peek().CanContinue)
                {
                }
                if (wireResult != null)
                {
                    float result = (float)GlobalVariables.ExportData.Peek().ResultChange.Value;
                    Variable variable = new Variable
                    {
                        Value = result,
                        ValueType = typeof(float)
                    };
                    Variables.ValueFromWires[wireResult] = variable;
                    GlobalVariables.ExportData.Pop();
                }
            }

        }

        void ProccessLoop(ConfigurableWhileLoop loopBlock)
        {
            string wireResult = null;
            int loopIndex = -1;
            Variables.LoopNames[loopBlock.InterruptName]++;
            Variables.LoopStack.Push(loopBlock.InterruptName);
            ConfigurableWaitFor stopCondition = new ConfigurableWaitFor();

            foreach (var built in loopBlock.BuiltInMethod)
            {
                switch (built.CallType)
                {
                    case "LoopIndex":
                        foreach (var data in built.ConfigurableMethodCall.ConfigurableMethodTerminalList)
                        {
                            if (data.Terminal.Id == "Loop\\ Index" && data.Terminal.Wire != null)
                            {
                                wireResult = data.Terminal.Wire;
                            }
                        }
                        break;
                    case "StopCondition":
                        stopCondition.ConfigurablemethodTerminalList = built.ConfigurableMethodCall.ConfigurableMethodTerminalList;
                        stopCondition.Target = built.ConfigurableMethodCall.Target;
                        stopCondition.TerminalList = built.ConfigurableMethodCall.TerminalList;
                        break;
                }
            }
            if (stopCondition.Target == "StopNever\\.vix")
            {
                while (true)
                {
                    if (wireResult != null)
                        Variables.VariablesDictionary[wireResult] = new Variable
                        {
                            Value = ++loopIndex,
                            ValueType = typeof(int)
                        };
                    Run((loopBlock).DeserializedProgram);
                    if (Variables.Interrupt != null)
                    {
                        if (Variables.Interrupt == loopBlock.InterruptName)
                            Variables.Interrupt = null;
                        Variables.LoopStack.Pop();
                        return;
                    }
                }
            }
            if (stopCondition.Target == "StopIfTrue\\.vix")
            {
                bool stop = false;
                wireResult = null;
                foreach (var data in stopCondition.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Direction == "Do\\ Stop")
                    {
                        if (data.Terminal.Wire != null)
                            wireResult = data.Terminal.Wire;
                        else
                            stop = (bool)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                        break;
                    }
                }
                do
                {
                    if (wireResult != null)
                        Variables.VariablesDictionary[wireResult] = new Variable
                        {
                            Value = ++loopIndex,
                            ValueType = typeof(int)
                        };
                    Run((loopBlock).DeserializedProgram);
                    if (Variables.Interrupt != null)
                    {
                        if (Variables.Interrupt == loopBlock.InterruptName)
                            Variables.Interrupt = null;
                        Variables.LoopNames[loopBlock.InterruptName]--;
                        Variables.LoopStack.Pop();
                        return;
                    }
                    if (wireResult != null)
                        stop = (bool)Variable.WireToNeededType(wireResult, "Boolean").Value;
                } while (!stop);
                Variables.LoopNames[loopBlock.InterruptName]--;
                Variables.LoopStack.Pop();
                return;
            }
            if (stopCondition.Target == "StopAfterNumberIterations\\.vix")
            {
                int iter = 0;
                wireResult = null;
                foreach (var data in stopCondition.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Direction == "Iterations\\ To\\ Run")
                    {
                        if (data.Terminal.Wire != null)
                            wireResult = data.Terminal.Wire;
                        else
                            iter = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                    }
                }
                do
                {
                    if (wireResult != null)
                        Variables.VariablesDictionary[wireResult] = new Variable
                        {
                            Value = ++loopIndex,
                            ValueType = typeof(int)
                        };
                    Run((loopBlock).DeserializedProgram);
                    if (Variables.Interrupt != null)
                    {
                        if (Variables.Interrupt == loopBlock.InterruptName)
                            Variables.Interrupt = null;
                        Variables.LoopNames[loopBlock.InterruptName]--;
                        Variables.LoopStack.Pop();
                        return;
                    }
                    if (wireResult != null)
                        iter = (int)Variable.WireToNeededType(wireResult, "Int32").Value;
                } while (iter < loopIndex); //TODO <=?
                Variables.LoopNames[loopBlock.InterruptName]--;
                Variables.LoopStack.Pop();
                return;
            }
            do
            {
                if (wireResult != null)
                    Variables.VariablesDictionary[wireResult] = new Variable
                    {
                        Value = ++loopIndex,
                        ValueType = typeof(int)
                    };
                Run(loopBlock.DeserializedProgram);
                if (Variables.Interrupt != null)
                {
                    if (Variables.Interrupt == loopBlock.InterruptName)
                        Variables.Interrupt = null;
                    Variables.LoopNames[loopBlock.InterruptName]--;
                    Variables.LoopStack.Pop();
                    return;
                }
            } while (!GlobalVariables.ExportData.Peek().CanContinue);
            Variables.LoopNames[loopBlock.InterruptName]--;
            Variables.LoopStack.Pop();
        }

        void ProccessSwitch(PairedConfigurableMethodCall switchParam, ConfigurableFlatCaseStructure caseStructure)
        {
            string def = caseStructure.Id;
            Case defCase = null;
            Variable varCompare = null;
            Type type = typeof(string);
            if (switchParam.Target == "CaseSelector_String\\.vix")
            {
                foreach (var data in switchParam.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Id == "String")
                    {
                        if (data.Terminal.Wire != null)
                            varCompare = Variable.WireToNeededType(data.Terminal.Wire, data.ConfiguredValue);
                        else
                            varCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    }
                }
                foreach (var caseElement in caseStructure.CaseList)
                {
                    if (caseElement.Id == def)
                        defCase = caseElement;
                    if ((string)varCompare.Value == caseElement.Pattern)
                    {
                        Run(caseElement.DeserializedProgram);
                        return;
                    }
                }
                Run(defCase.DeserializedProgram);
                return;
            }
            if (switchParam.Target == "CaseSelector_Boolean\\.vix")
            {
                foreach (var data in switchParam.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Id == "Boolean")
                    {
                        if (data.Terminal.Wire != null)
                            varCompare = Variable.WireToNeededType(data.Terminal.Wire, data.ConfiguredValue);
                        else
                            varCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    }
                }
                foreach (var caseElement in caseStructure.CaseList)
                {
                    if (caseElement.Id == def)
                        defCase = caseElement;
                    if ((bool)varCompare.Value == bool.Parse(caseElement.Pattern))
                    {
                        Run(caseElement.DeserializedProgram);
                        return;
                    }
                }
                Run(defCase.DeserializedProgram);
                return;
            }
            if (switchParam.Target == "CaseSelector_Numeric\\.vix")
            {
                foreach (var data in switchParam.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Id == "Numeric")
                    {
                        if (data.Terminal.Wire != null)
                            varCompare = Variable.WireToNeededType(data.Terminal.Wire, data.ConfiguredValue);
                        else
                            varCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    }
                }
                foreach (var caseElement in caseStructure.CaseList)
                {
                    if (caseElement.Id == def)
                        defCase = caseElement;
                    if ((int)varCompare.Value == int.Parse(caseElement.Pattern))
                    {
                        Run(caseElement.DeserializedProgram);
                        return;
                    }
                }
                Run(defCase.DeserializedProgram);
                return;
            }
            var param = new ConfigurableMethodCall
            {
                ConfigurableMethodTerminalList = switchParam.ConfigurablemethodTerminalList,
                Target = switchParam.Target,
                TerminalList = switchParam.TerminalList
            };
            ProccessSensors(param);
            if (switchParam.Target.Contains("Compare"))
            {
                foreach (var caseElement in caseStructure.CaseList)
                {
                    if ((bool)GlobalVariables.ExportData.Peek().ResultCompare.Value == bool.Parse(caseElement.Pattern))
                    {
                        Run(caseElement.DeserializedProgram);
                        return;
                    }
                }
            }
            else
            {
                foreach (var caseElement in caseStructure.CaseList)
                {
                    if (Math.Abs((float)GlobalVariables.ExportData.Peek().ResultChange.Value - float.Parse(caseElement.Pattern)) < 1e-3)
                    {
                        Run(caseElement.DeserializedProgram);
                        return;
                    }
                }

            }
        }

        void ProccessInterrupt(ConfigurableMethodCall interruptBlock)
        {
            foreach (var data in interruptBlock.ConfigurableMethodTerminalList)
            {
                if (data.Terminal.Id == "InterruptName")
                {
                    if (Variables.LoopNames[data.ConfiguredValue] > 0)
                        Variables.Interrupt = data.ConfiguredValue;
                    return;
                }
            }
        }

        void ProccessSensors(ConfigurableMethodCall sensorBlock)
        {
            SensorTypes sensorType = GetProgramType.GetSensorType(sensorBlock.Target);

            if (sensorBlock.Target.Contains("Calibrate"))
            {
                SetMessage.SetMessageSensor();
                return;
            }
            if (sensorBlock.Target == "RotationReset\\.vix")
            {
                string port = null;
                SetMessage.SetMessageSensor();
                foreach (var data in sensorBlock.ConfigurableMethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "MotorPort":
                            port = data.ConfiguredValue;
                            break;
                    }
                }
                Action.NewGet(false, SensorTypes.LargeMotorRotations, false, port[2], reset: true);
                return;
            }
            if (sensorBlock.Target == "TimerReset\\.vix")
            {
                Variable timer = null;
                SetMessage.SetMessageSensor();
                foreach (var data in sensorBlock.ConfigurableMethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "Timer":
                            if (data.Terminal.Wire != null)
                                timer = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                timer = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                    }
                }
                Action.NewGet(false, SensorTypes.Timer, false, timer: timer, reset: true);
                return;
            }
            if (sensorBlock.Target.Contains("Compare"))
            {
                string wireResultfloat = null, wireResultBool = null, port = null;
                Variable timer = null;
                int comparasion = 0;
                Variable valueCompare = null;
                Variable arrayCompare = null, buttons = null;
                foreach (var data in sensorBlock.ConfigurableMethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "MotorPort":
                        case "Port":
                            port = data.ConfiguredValue;
                            break;
                        case "Buttons":
                            if (data.Terminal.Wire != null)
                                buttons = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                buttons = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Timer":
                            if (data.Terminal.Wire != null)
                                timer = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                timer = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Comparasion":
                            if (data.Terminal.Wire != null)
                                comparasion = (int)Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType).Value;
                            else
                                comparasion = (int)Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType).Value;
                            break;
                        case "Action":
                        case "ThresholdDegrees":
                        case "ThresholdRotations":
                        case "ThresholdSpeed":
                        case "Threshold":
                        case "How\\ Long":
                        case "Pressed\\,\\ Released\\ or\\ Bumped":
                            if (data.Terminal.Wire != null)
                                valueCompare = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                valueCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Set\\ of\\ colors":
                            if (data.Terminal.Wire != null)
                                arrayCompare = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                arrayCompare = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Value":
                        case "Degrees":
                        case "Rotations":
                        case "Speed":
                        case "Timer\\ Value":
                        case "Color":
                        case "Proximity":
                        case "Distance":
                        case "DistanceInches":
                            if (data.Terminal.Wire != null)
                                wireResultfloat = data.Terminal.Wire;
                            break;
                        case "State":
                        case "Result":
                            if (data.Terminal.Wire != null)
                                wireResultBool = data.Terminal.Wire;
                            break;
                    }
                }
                SetMessage.SetMessageSensor();
                Action.NewGet(false, sensorType, true, port == null ? '0' : port[2], (ComparasionTypes)comparasion, valueCompare, arrayCompare, buttons, timer);
                //TODO сон
                while (!GlobalVariables.ExportData.Peek().CanContinue)
                {
                }
                if (wireResultfloat != null)
                {
                    float result = (float)GlobalVariables.ExportData.Peek().ResultChange.Value;
                    Variable variable = new Variable
                    {
                        Value = result,
                        ValueType = typeof(float)
                    };
                    Variables.ValueFromWires[wireResultfloat] = variable;
                    GlobalVariables.ExportData.Pop();
                }
                if (wireResultBool != null)
                {
                    bool result = (bool)GlobalVariables.ExportData.Peek().ResultCompare.Value;
                    Variable variable = new Variable
                    {
                        Value = result,
                        ValueType = typeof(bool)
                    };
                    Variables.ValueFromWires[wireResultBool] = variable;
                    GlobalVariables.ExportData.Pop();
                }
            }
            else
            {
                string wireResult = null, port = null;
                Variable timer = null;
                foreach (var data in sensorBlock.ConfigurableMethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "MotorPort":
                        case "Port":
                            port = data.ConfiguredValue;
                            break;
                        case "Timer":
                            if (data.Terminal.Wire != null)
                                timer = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                            else
                                timer = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                            break;
                        case "Value":
                        case "Color":
                        case "Proximity":
                        case "Degrees":
                        case "Rotations":
                        case "Speed":
                        case "Timer\\ Value":
                        case "State":
                        case "Distance":
                        case "DistanceInches":
                            if (data.Terminal.Wire != null)
                                wireResult = data.Terminal.Wire;
                            break;
                    }
                }
                Action.NewGet(false, sensorType, false, port[2], timer: timer);
                //TODO сон
                while (!GlobalVariables.ExportData.Peek().CanContinue)
                {
                }
                if (wireResult != null)
                {
                    float result = (float)GlobalVariables.ExportData.Peek().ResultChange.Value;
                    Variable variable = new Variable
                    {
                        Value = result,
                        ValueType = typeof(float)
                    };
                    Variables.ValueFromWires[wireResult] = variable;
                    GlobalVariables.ExportData.Pop();
                }
            }
        }

        void ProccessData(ConfigurableMethodCall dataBlock)
        {
            string name = null, wireResult = null;
            List<Variable> valueIn = new List<Variable>();

            if (dataBlock.Target.Contains("X3"))
            {
                foreach (var data in dataBlock.ConfigurableMethodTerminalList)
                {
                    switch (data.Terminal.Id)
                    {
                        case "name":
                            name = data.ConfiguredValue;
                            break;
                        case "valueIn":
                            if (data.Terminal.Wire != null)
                                valueIn.Add(Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType));
                            else
                                valueIn.Add(Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType));
                            break;
                        case "valueOut":
                            if (data.Terminal.Wire != null)
                                wireResult = data.Terminal.Wire;
                            break;
                    }
                }
                if (name == null)
                {
                    if (wireResult != null)
                        Variables.ValueFromWires[wireResult] = valueIn[0];
                    return;
                }
                if (valueIn.Count == 0)
                {
                    if (wireResult != null)
                        Variables.ValueFromWires[wireResult] = Variables.VariablesDictionary[name];
                    return;
                }
                Variables.VariablesDictionary[name] = valueIn[0];
                return;
            }

            Variable valueOut = null, changingValue = null,
                index = null, decimals = null, lBound = null,
                uBound = null, percent = null;
            Variable basePower = null, exponent = null;

            foreach (var data in dataBlock.ConfigurableMethodTerminalList)
            {
                switch (data.Terminal.Id)
                {
                    case "arrayInNumeric":
                    case "arrayInBoolean":
                        if (data.Terminal.Wire != null)
                            changingValue = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            changingValue = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "valueIn":
                    case "A":
                    case "B":
                    case "C":
                    case "X":
                    case "Y":
                    case "Input":
                    case "x":
                    case "y":
                    case "Test\\ Value":
                        if (data.Terminal.Wire != null)
                            valueIn.Add(Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType));
                        else
                            valueIn.Add(Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType));
                        break;
                    case "Lower\\ Bound":
                    case "Lower":
                        if (data.Terminal.Wire != null)
                            lBound = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            lBound = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Upper\\ Bound":
                    case "Upper":
                        if (data.Terminal.Wire != null)
                            uBound = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            uBound = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Percent\\ True":
                        if (data.Terminal.Wire != null)
                            percent = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            percent = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Index":
                        if (data.Terminal.Wire != null)
                            index = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            index = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Number\\ of\\ Decimals":
                        if (data.Terminal.Wire != null)
                            decimals = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            decimals = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Base":
                        if (data.Terminal.Wire != null)
                            basePower = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            basePower = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "Exponent":
                        if (data.Terminal.Wire != null)
                            exponent = Variable.WireToNeededType(data.Terminal.Wire, data.Terminal.DataType);
                        else
                            exponent = Variable.StringToNeededType(data.ConfiguredValue, data.Terminal.DataType);
                        break;
                    case "arrayOutNumeric":
                    case "arrayOutBoolean":
                    case "valueOut":
                    case "Size":
                    case "Result":
                    case "Output\\ Result":
                    case "Number":
                        if (data.Terminal.Wire != null)
                            wireResult = data.Terminal.Wire;
                        break;
                }
            }
            valueOut = Maths.MathOperation(dataBlock.Target, valueIn, changingValue, index, decimals, lBound, uBound, percent,
                basePower, exponent);
            if (wireResult != null)
                Variables.ValueFromWires[wireResult] = valueOut;
        }

        void ProccessPersonalBlock(ConfigurableMethodCall personalBlock)
        {
            //TODO ConfigurableMegaAccessor
        }
    }
}
