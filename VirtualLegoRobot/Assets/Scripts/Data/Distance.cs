
namespace Assets.Scripts.Data
{
    public class Distance
    {
        public double Value { get; private set; }
        public UnitTypes Unit { get; set; }

        public Distance(double value, UnitTypes unit) { Value = value; Unit = unit; }
    }
}
