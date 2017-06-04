using System;
using System.IO;
using System.IO.Compression;
using EV3PDeserializeLib;
using System.Collections.Generic;
using EV3PDeserializeLib.Interfaces;

namespace VirtualLegoRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к распаковывему файлу");
            string filename;
            //filename = Console.ReadLine();
            //string extractPath = Unarchiver.EV3Extract(filename);
            List<DeserializedProgram> deserializedProgram = new List<DeserializedProgram>();
            string extractPath = Console.ReadLine();
            if (extractPath != null)
            {
                Console.WriteLine(true);
                string[] programFiles = Directory.GetFiles(extractPath, "*.ev3p");
                foreach (string programName in programFiles)
                {
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

                    deserializedProgram.Add(SourceFile.Deserialize(programName));
                }
            }
            else
            {
                Console.WriteLine(false);
            }
        }

        void Run(DeserializedProgram program)
        {
            foreach (Wire wire in program.TurnRunning)
            {
                DoSmth(program.WiresDictionary[wire.Id]);
            }
        }

        void DoSmth(IBlock block)
        {
            if (block is StartBlock)
            {
                //вывод сообщения - программа запущена
                return;
            }
            if (block is ConfigurableMethodCall)
            {
                //get Name
                //get Ports
                //get Values
                //set getting is true or setting is true
                //wait answer
                return;
            }
            if (block is ConfigurableWaitFor)
            {
                //set getting is true
                while (true) ; //value != condition
            }
            if (block is ConfigurableWhileLoop)
            {
                //set getting is true
                do //value != condition
                {
                    DeserializedProgram dp = new DeserializedProgram();
                    //dp - get data from loop
                    //Run(((ConfigurableWhileLoop)block));
                } while (true);
            }
            if (block is PairedConfigurableMethodCall)
            {
                //get value
                //switch (value)
                {
                    //case by cases
                    //dp
                    //Run()
                }

            }
        }
    }
}