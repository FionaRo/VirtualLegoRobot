using System.Collections.Generic;

namespace Assets.Scripts.Data
{
    public static class Variables
    {

        public static Dictionary<string, Variable> VariablesDictionary = new Dictionary<string, Variable>();
       
        public static Dictionary<string, Variable> ValueFromWires = new Dictionary<string, Variable>();

        public static Stack<string> LoopStack = new Stack<string>();
        public static Dictionary<string, int> LoopNames = new Dictionary<string, int>();
        public static string Interrupt = null;

    }
}
