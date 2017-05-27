using System;
using System.IO;
using Ionic.Zip;

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
                ZipFile zip = ZipFile.Read(filename);
                zip.ExtractAll(extractPath);
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
