
namespace Assets.Scripts.Data
{
    public class Get
    {
        public char Port { get; set; }
        public SensorTypes SensorType { get; set; } //TODO проверка на соответствие типов
        public int Value { get; set; }
    }
}
