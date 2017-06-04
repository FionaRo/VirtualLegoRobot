using System;
using System.IO;
using Ionic.Zip;

namespace Assets.Scripts.ProgramScripts
{
    public class Unarchiver
    {
        public static string EV3Extract(string filename)
        {
            string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            userName = userName.Split('\\')[1];
            filename = filename.Replace("/", "\\");
            string[] splitPath = filename.Split('\\');
            string extractPath = "C:\\Users\\" + userName + "\\Documents\\LegoVirtualRobot\\ExtractedFiles\\";
            extractPath += splitPath[splitPath.Length - 1];
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
