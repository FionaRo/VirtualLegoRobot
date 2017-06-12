
namespace Assets.Scripts.Data
{
    public class Get
    {
        //set
        public Variable Buttons;
        public Variable Timer;
        public char Port;
        public SensorTypes SensorType; //TODO проверка на соответствие типов
        public ComparasionTypes Comparasion;
        public bool IsComparsion;
        public Variable ValueCompare;
        public Variable ArrayCompare;
        public bool IsWait;
        

        //get
        public bool CanContinue;
        public Variable ResultChange = new Variable() { ValueType = typeof(float) };
        public Variable ResultCompare = new Variable() { ValueType = typeof(bool) };

    }
}
