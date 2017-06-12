

namespace Assets.Scripts.Data
{
    public class Motor
    {
        public char Port { get; private set; }
        public float Power { get; private set; }

        public Motor(char port, float power)
        {
            Port = port;
            Power = power;
        }
    }
}
