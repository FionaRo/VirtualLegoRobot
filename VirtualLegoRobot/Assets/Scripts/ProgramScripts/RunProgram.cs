using EV3PDeserializeLib;
using EV3PDeserializeLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public class RunProgram
    {
        Dictionary<string, DeserializedProgram> runningProgram;

        public RunProgram(string path)
        {
            string extractPath = Unarchiver.EV3Extract(path);
            runningProgram = new Dictionary<string, DeserializedProgram>();
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
            //GlobalVariable.CanContinue = true;
            //GlobalVariable.ExportData = new Stack<Get>();
            //GlobalVariable.Pause = false;
            //GlobalVariable.Timer = 0.0;
            
            Run(runningProgram[programName]);
        }

        void Run(DeserializedProgram program)
        {
            foreach (Wire wire in program.TurnRunning)
            {
                if (program.WiresDictionary[wire.Id] is PairedConfigurableMethodCall)
                {
                    PairedConfigurableMethodCall PCMCblock = program.WiresDictionary[wire.Id] as PairedConfigurableMethodCall;
                    Process(PCMCblock, program.Switch[PCMCblock.PairedStructure]);
                }
                else
                {
                    Process(program.WiresDictionary[wire.Id]);
                }
            }
        }

        void Process(IBlock block, ConfigurableFlatCaseStructure cases = null)
        {
            //TODO buttons не имеет порта!
            if (block is StartBlock)
            {
                //TODO вывод сообщения - программа запущена
                return;
            }
            if (block is ConfigurableMethodCall)
            {
                ConfigurableMethodCall CMCblock = block as ConfigurableMethodCall;
                //если моторы
                if (InternalProgramsNames.motorsNames.Contains(CMCblock.Target))
                {
                    string ports = null, unitDistance = null;
                    double distance = 0;
                    int speed1 = 0, speed2 = 0, steering = 101;
                    foreach (var data in CMCblock.ConfigurableMethodTerminalList)
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
                                distance = Double.Parse(data.ConfiguredValue);
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
                                break;
                            default:
                                break;
                        }
                    }
                    if (steering != 101) // блок рулевого управления преобразовываем к блоку независимого
                    {

                        if (steering >= 0)
                            speed2 = ((50 - steering) / 50) * speed1;
                        else
                        {
                            speed2 = speed1;
                            speed1 = ((50 - Math.Abs(steering)) / 50) * speed2;
                        }
                    }
                    //Постройка сообщения
                    if (steering != 101)
                    {
                        GlobalVariable.message = "Движение моторов. Порты: " + ports + ". Мощность: " + speed1 + ". Направление: " + steering + ". Расстояние: ";

                    }
                    else
                    {
                        if (ports.Length == 5)
                            GlobalVariable.message = "Движение моторов. Порты: " + ports + ". Мощность левого: " + speed1 + ". Мощность правого: " + speed2 + ". Расстояние: ";
                        else
                            GlobalVariable.message = "Движение моторов. Порт: " + ports + ". Мощность: " + speed1 + ". Расстояние: ";
                    }
                    if (unitDistance == null)
                    {
                        GlobalVariable.message += "нет.\n";
                    }
                    else
                    {
                        GlobalVariable.message += distance;
                        string convertingDistance = Convert.ToString(distance);
                        int ending;
                        switch (convertingDistance[convertingDistance.Length - 1])
                        {
                            case '1':
                                ending = 1;
                                break;
                            case '2':
                            case '3':
                            case '4':
                                ending = 2;
                                break;
                            default:
                                ending = 3;
                                break;
                        }
                        switch (unitDistance)
                        {
                            case "Seconds":
                                switch (ending)
                                {
                                    case 1:
                                        GlobalVariable.message += " секунда.\n";
                                        break;
                                    case 2:
                                        GlobalVariable.message += " секунды.\n";
                                        break;
                                    case 3:
                                        GlobalVariable.message += " секунд.\n";
                                        break;
                                }
                                break;
                            case "Rotations":
                                switch (ending)
                                {
                                    case 1:
                                        GlobalVariable.message += " оборт.\n";
                                        break;
                                    case 2:
                                        GlobalVariable.message += " оборота.\n";
                                        break;
                                    case 3:
                                        GlobalVariable.message += " оборотов.\n";
                                        break;
                                }
                                break;
                            case "Degrees":
                                switch (ending)
                                {
                                    case 1:
                                        GlobalVariable.message += " градус.\n";
                                        break;
                                    case 2:
                                        GlobalVariable.message += " градуса.\n";
                                        break;
                                    case 3:
                                        GlobalVariable.message += " градусов.\n";
                                        break;
                                }
                                break;
                        }
                    }

                    Action.NewSet(ports, speed1, speed2, distance, unitDistance);
                    //нужен get
                    if (unitDistance != null)
                    {
                        if (Math.Abs(speed1) > Math.Abs(speed2))
                        {
                            Action.NewGet(ports[2], SensorTypes.LARGE_MOTOR);
                        }
                        else
                        {
                            Action.NewGet(ports[4], SensorTypes.LARGE_MOTOR);
                        }
                        //ждем пока мотор не доедет
                        switch (unitDistance)
                        {
                            case "Seconds":
                                double time = GlobalVariable.timer;
                                while (GlobalVariable.timer - time < distance) ;
                                break;
                            case "Rotations":
                                while (distance < GlobalVariable.exportData.Peek().Value / 360.0) ;
                                break;
                            case "Degrees":
                                while (distance < GlobalVariable.exportData.Peek().Value) ;
                                break;
                        }
                    }
                    return;
                }
                if (InternalProgramsNames.sensorsNames.Contains(CMCblock.Target)) { return; } //TODO wires - получение значений с датчиков
                if (InternalProgramsNames.variablesNames.Contains(CMCblock.Target)) { return; } //TODO wires - переменные
                    return;
            }
            if (block is ConfigurableWaitFor)
            {
                //TODO сообщение
                ConfigurableWaitFor CWFblock = block as ConfigurableWaitFor;
                if (CWFblock.Target == "TimeCompare\\.vix")
                {
                    double howLong = 0.0;
                    foreach(var data in CWFblock.ConfigurablemethodTerminalList)
                    {
                        if (data.Terminal.Id == "How\\ Long")
                        {
                            howLong = Double.Parse(data.ConfiguredValue);
                            break;
                        }
                    }
                    double time = GlobalVariable.timer;
                    while (GlobalVariable.timer - time < howLong) ;
                    return;
                }
                if (CWFblock.Target.Contains("Compare"))
                {
                    //TODO получение типа сенсора
                    string ports = null, comparasion = null, threshold = null; //TODO сравнение со значением или принадлежность массиву данных
                    foreach (var data in CWFblock.ConfigurablemethodTerminalList)
                    {
                        if (data.Terminal.Id == "Port")
                        {
                            ports = data.ConfiguredValue;
                        }
                    }
                    Action.NewGet(ports[2]);
                    //switch по знакам сравнения или пробег по массиву
                    while (GlobalVariable.exportData.Peek().Value != 0) ;
                    return;
                }
                else
                {
                    //TODO получение типа сенсора
                    string ports = null;
                    foreach (var data in CWFblock.ConfigurablemethodTerminalList)
                    {
                        if (data.Terminal.Id == "Port")
                        {
                            ports = data.ConfiguredValue;
                            break;
                        }
                    }
                    Action.NewGet(ports[2]);
                    GlobalVariable.canContinue = false;
                    while (!GlobalVariable.canContinue) ;
                    int curValue = GlobalVariable.exportData.Peek().Value;
                    while (curValue == GlobalVariable.exportData.Peek().Value) ;
                    return;
                }
            }
            if (block is ConfigurableWhileLoop)
            {
                //TODO interruptName и счетчик
                BuiltInMethod stopCondition = (block as ConfigurableWhileLoop).BuiltInMethod[1];
                //TODO значение на сравнение - массив или Int
                string port = null;
                foreach(var data in stopCondition.ConfigurableMethodCall.ConfigurableMethodTerminalList)
                {
                    if (data.Terminal.Id == "Port")
                    {
                        port = data.ConfiguredValue;
                    }
                }
                Action.NewGet(port[2]);
                int loopIndex = 0; //TODO нужна глобально
                while (GlobalVariable.exportData.Peek().Value != 0)
                {
                    Run((block as ConfigurableWhileLoop).DeserializedProgram);
                    loopIndex++;
                }
            }
            if (block is PairedConfigurableMethodCall)
            {
                PairedConfigurableMethodCall PCMCblock = block as PairedConfigurableMethodCall;
                string port = null;
                foreach(var data in PCMCblock.ConfigurablemethodTerminalList)
                {
                    if (data.Terminal.Id =="Ports")
                    {
                        port = data.ConfiguredValue;
                    }
                }
                Action.NewGet(port[2]);
                int value = GlobalVariable.exportData.Peek().Value;
                foreach(var Case in cases.CaseList)
                {
                    //либо bool либо число
                    if (Int32.Parse(Case.Pattern) == value)
                    {
                        Run(Case.DeserializedProgram);
                        break;
                    }
                }
            }
        }
    }
}
