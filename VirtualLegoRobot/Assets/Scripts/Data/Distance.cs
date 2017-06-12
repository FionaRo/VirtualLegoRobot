
namespace Assets.Scripts.Data
{
    public class Distance
    {
        public float Value { get; private set; }
        public UnitTypes Unit { get; set; }

        public Distance(float value, UnitTypes unit) { Value = value; Unit = unit; }
    }
}
