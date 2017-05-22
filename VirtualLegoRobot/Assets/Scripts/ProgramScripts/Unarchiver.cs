using System;
using System.IO;
using System.IO.Compression;

namespace Assets.Scripts.ProgramScripts
{
    public class Unarchiver
    {
        public static string EV3Extract(string filename)
        {
            string[] splitPath = filename.Split('\\');
            string extractPath = "ExtractedFiles\\";
            extractPath = extractPath.Insert(extractPath.Length, splitPath[splitPath.Length - 1]);
            if (Directory.Exists(extractPath))
            {
                Directory.Delete(extractPath, true);
            }

            try
            {
                //TODO извлечение файлов из архива .NET 4.0
                //ZipFile.ExtractToDirectory(filename, extractPath); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return extractPath;
        }
    }
}
