using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class GlobalVariable
    {
        public static Stack<Get> exportData = new Stack<Get>();
        public static Set importData = null; //задать и очистить
        public static bool canContinue = true; //если false - меняем get и возвращаем true
        public static string message = null; //вывести и очистить
        public static bool pause = false;

        public static double timer = 0.0;
    }
}
