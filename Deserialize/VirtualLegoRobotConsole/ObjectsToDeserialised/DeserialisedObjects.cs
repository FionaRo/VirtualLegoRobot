using System;
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
            SourceFile sourceFile;
            //sourceFile = (SourceFile)serializer.Deserialize(reader);
            //fs.Close();

            //SourceFile sf = new SourceFile()
            //{
            //    namespaceDSR = new Namespace()
            //    {
            //        projectName = "Project",
            //        VirtualInstrument = new VirtualInstrument()
            //        {
            //            s = "False"
            //        }
            //    }
            //};

            StreamReader reader = new StreamReader(path);
            
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
