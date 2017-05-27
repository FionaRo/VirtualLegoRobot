using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class GlobalVariables
    {
        public static Stack<Get> ExportData = new Stack<Get>();
        public static Set ImportData = null; //задать и очистить
        public static string Message = null; //вывести и очистить
    }
}
