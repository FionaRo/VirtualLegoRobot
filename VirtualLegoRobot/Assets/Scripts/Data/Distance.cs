
namespace Assets.Scripts.Data
{
    public class Distance
    {
        public double Value { get; set; }
        UnitsTypes Unit { get; set; }

        public Distance(double value, UnitsTypes unit) { Value = value; Unit = unit; }
    }
}
