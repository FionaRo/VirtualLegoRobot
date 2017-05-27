
namespace Assets.Scripts.Data
{
    public class Get
    {
        //set
        public int[] Buttons;
        public int Timer;
        public char Port;
        public SensorTypes SensorType; //TODO проверка на соответствие типов
        public ComparasionTypes Comparasion;
        public bool IsComparsion;
        public double ValueCompare;
        public int[] ArrayCompare;
        public bool IsWait;
        public bool Reset;

        //get
        public bool CanContinue;
        public double ResultChange;
        public bool ResultCompare;

    }
}
