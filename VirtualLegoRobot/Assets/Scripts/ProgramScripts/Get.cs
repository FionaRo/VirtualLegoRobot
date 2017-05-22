using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProgramScripts
{
    public class Get
    {
        public char Port { get; set; }
        public SensorTypes SensorType { get; set; } //TODO проверка на соответствие типов
        public int Value { get; set; }
    }
}
