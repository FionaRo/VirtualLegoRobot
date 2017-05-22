using EV3PDeserializeLib;
using EV3PDeserializeLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace Assets.Scripts.ProgramScripts
{
    public class RunProgram
    {
        Dictionary<string, DeserializedProgram> runningProgram;

        public RunProgram(string path)
        {
            string extractPath = Unarchiver.EV3Extract(path);
            runningProgram = new Dictionary<string, DeserializedProgram>;
            if (extractPath != null)
            {
                string[] programFiles = Directory.GetFiles(extractPath, "*.ev3p");
                foreach(string programName in programFiles)
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
                    runningProgram[programName] = SourceFile.Deserialize(programName);
                }
            }
            else
            {
                //TODO вывод ошибки при распаковке файла
            }
        }

        public void Start(string programName)
        {
            GlobalVariable.CanContinue = true;
            GlobalVariable.ExportData = new Stack<Get>();
            Run(runningProgram[programName]);
        }

        void Run(DeserializedProgram program)
        {
            foreach (Wire eire in program.TurnRunning)
            {

            }
        }

        void Process(IBlock block, ConfigurableFlatCaseStructure cases = null)
        {
            if (block is StartBlock)
            {
                //TODO вывод сообщения - программа запущена
                return;
            }
            if (block is ConfigurableMethodCall)
            {
                ConfigurableMethodCall CMCblock =  block as ConfigurableMethodCall;
                if (InternalProgramsNames.motorsNames.Contains(CMCblock.Target)) //если моторы
                {
                    //TODO сообщение
                    string ports = null, unitDistance = null;
                    int speed1 = 0, speed2 = 0, distance = 0, steering = 101;
                    foreach(var data in CMCblock.ConfigurableMethodTerminalList)
                    {
                        switch(data.Terminal.Id)
                        {
                            case "MotorPort":
                            case "Ports":
                                ports = data.ConfiguredValue;
                                break;
                            case "Seconds":
                            case "Degrees":
                            case "Rotations":
                                unitDistance = data.Terminal.Id;
                                distance = Int32.Parse(data.ConfiguredValue);
                                break;
                            case "Speed":
                                speed1 = Int32.Parse(data.ConfiguredValue);
                                break;
                            case "Speed\\ Left":
                                speed1 = Int32.Parse(data.ConfiguredValue);
                                break;
                            case "Speed\\ Right":
                                speed2 = Int32.Parse(data.ConfiguredValue);
                                break;
                            case "Steering":
                                steering = Int32.Parse(data.ConfiguredValue);
                            default:
                                break;
                        }
                        if (steering!=101)
                        {
                            if (steering >= 0)
                                speed2 = ((50 - steering) / 50) * speed1;
                            else
                            {
                                speed2 = speed1;
                                speed1 = ((50 - Math.Abs(steering)) / 50) * speed2;
                            }
                        }
                    }
                    SetAction.NewSet(ports, speed1, speed2, distance, unitDistance);
                }
            }
        }
    }
}
