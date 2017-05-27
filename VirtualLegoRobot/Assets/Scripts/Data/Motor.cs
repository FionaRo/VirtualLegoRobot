

namespace Assets.Scripts.Data
{
    public class Motor
    {
        public char Port { get; private set; }
        public double Power { get; private set; }

        public Motor(char port, double power)
        {
            Port = port;
            Power = power;
        }
    }
}
