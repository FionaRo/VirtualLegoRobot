using System;
using System.IO;
using Ionic.Zip;

namespace VirtualLegoRobotConsole
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
                // Specifying Console.Out here causes diagnostic msgs to be sent to the Console
                // In a WinForms or WPF or Web app, you could specify nothing, or an alternate
                // TextWriter to capture diagnostic messages.

                var options = new ReadOptions { StatusMessageWriter = System.Console.Out };
                using (ZipFile zip = ZipFile.Read(filename, options))
                {
                    // This call to ExtractAll() assumes:
                    //   - none of the entries are password-protected.
                    //   - want to extract all entries to current working directory
                    //   - none of the files in the zip already exist in the directory;
                    //     if they do, the method will throw.
                    zip.ExtractAll(extractPath);
                }
            }
            catch (System.Exception ex1)
            {
                System.Console.Error.WriteLine("exception: " + ex1);
            }
            return extractPath;
        }
    }
}
