using System.IO;
using YAXLib;

namespace EV3PDeserializeLib
{
    public class SourceFile
    {
        [YAXSerializeAs("Namespace")]
        public Namespace Namespace { get; set; }

        public static SourceFile Deserialize(string filename)
        {
            YAXSerializer serializer = new YAXSerializer(typeof(SourceFile));
            try
            {
                object deserializedObject = serializer.DeserializeFromFile(filename);
                if (deserializedObject!=null)
                {
                    SourceFile sourceFile = (SourceFile)deserializedObject;
                    return sourceFile;
                }
                else
                {
                    throw new IOException("Deserializing XML is failed");
                }
            }
            catch
            {
                throw new IOException("Deserializing XML is failed");
            }
        }
    }
}
