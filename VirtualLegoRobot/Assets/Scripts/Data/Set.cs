

namespace Assets.Scripts.Data
{
    public class Set
    {
        public Motor Motor1 { get; set; }
        public Motor Motor2 { get; set; }
        public Distance Distance { get; set; }

        public Set()
        {
            Motor1 = Motor2 = null;
            Distance = null;
        }
    }
}
