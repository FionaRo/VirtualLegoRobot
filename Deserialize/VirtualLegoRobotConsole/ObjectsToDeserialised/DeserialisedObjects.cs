using System;
using System.Collections.Generic;
using System.IO;
using YAXLib;

namespace VirtualLegoRobotConsole.ObjectsToDeserialised
{
    public class DeserialisedObjects
    {
        // private SourceFile sourceFile;


        public void Deserialised(string path)
        {
            Console.WriteLine("Reading with XmlReader");
            //XmlSerializer serializer = new XmlSerializer(typeof(SourceFile));
            //FileStream fs = new FileStream(path, FileMode.Open);
            //XmlReader reader = XmlReader.Create(fs);
            //sourceFile = (SourceFile)serializer.Deserialize(reader);
            //fs.Close();

            //Wire wi = new Wire();
            //wi.Id = "1";
            //List<Wire> w = new List<Wire>();
            //w.Add(wi);
            //wi.Id = "2";
            //w.Add(wi);
            //wi.Id = "3";
            //w.Add(wi);
            //SourceFile sf = new SourceFile()
            //{
            //    Namespace = new Namespace()
            //    {
            //        ProjectName = "Project",
            //        VirtualInstrument = new VirtualInstrument()
            //        {
            //            BlockDiagram = new BlockDiagram()
            //            {
            //                WireList = new List<Wire>(w)
            //            }
            //        }
            //    }
            //};

            StreamReader reader = new StreamReader(path);
            SourceFile sourceFile;

            YAXSerializer serializer = new YAXSerializer(typeof(SourceFile));
            //string someString = serializer.Serialize(sf);
            try
            {
                object o = serializer.Deserialize(reader);
                if (o != null)
                {
                    sourceFile = (SourceFile)o;
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            Console.WriteLine("Done");
            //StreamWriter writer = new StreamWriter("test.xml");
            //writer.Write(someString);
            //writer.Close();
        }
    }
}
