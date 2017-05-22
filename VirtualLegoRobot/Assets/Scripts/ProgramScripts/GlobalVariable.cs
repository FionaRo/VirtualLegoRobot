using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProgramScripts
{
    public static class GlobalVariable
    {
        public static Stack<Get> ExportData { get; set; }
        public static Set ImportData { get; set; }
        public static bool CanContinue { get; set; }
    }
}
