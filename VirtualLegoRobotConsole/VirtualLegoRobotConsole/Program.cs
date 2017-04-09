using System;
using System.IO;
using System.IO.Compression;
using EV3PDeserializeLib;
using System.Collections.Generic;

namespace VirtualLegoRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите путь к распаковывему файлу");
            string filename;
            filename = Console.ReadLine();
            string extractPath = Unarchiver.EV3Extract(filename);
            List<SourceFile> deserializedProgram = new List<SourceFile>;
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
    }
}