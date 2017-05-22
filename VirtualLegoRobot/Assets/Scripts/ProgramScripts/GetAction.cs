
using System.Collections.Generic;

namespace Assets.Scripts.ProgramScripts
{
    public class GetAction
    {
        public static void NewGet(string port)
        {
            Get get = new Get()
            {
                Port = port[2]
            };
            GlobalVariable.ExportData.Push(get);
        }
    }
}
