using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using VirtualLegoRobotConsole.ObjectsToDeserialised;
using YAXLib;

namespace VirtualLegoRobotConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DeserialisedObjects DSRobject = new DeserialisedObjects();
            string path = "C:\\Users\\Рина\\Desktop\\Program.ev3p";
            string textFile;

            using (StreamReader reader = new StreamReader(path))
            {
                textFile = reader.ReadToEnd();
                //textFile = textFile.Replace("xmlns=\"http://www.ni.com/SourceModel.xsd\"", "");
                textFile = textFile.Replace("xmlns=", "notlink=");
            }

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.Write(textFile);
            }

            //StreamReader read = new StreamReader(path);


            //Encoding ANSI = Encoding.GetEncoding(1252);
            //Encoding UTF8 = Encoding.UTF8;
            //byte[] utf8_bytes, ansi_bytes;

            //utf8_bytes = UTF8.GetBytes(read.ReadToEnd());
            //ansi_bytes = Encoding.Convert(UTF8, ANSI, utf8_bytes);
            //read.Close();

            //string ansi_str = ANSI.GetString(ansi_bytes);
            //StreamWriter write = new StreamWriter(path);

            //write.WriteLine(ansi_str);


            //write.Close();

            DSRobject.Deserialised(path);
        }
    }
}
