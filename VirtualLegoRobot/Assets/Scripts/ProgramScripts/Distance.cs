
namespace Assets.Scripts.ProgramScripts
{
    public class Distance
    {
        public int Value { get; set; }
        UnitsTypes Unit { get; set; }

        public Distance(int value, UnitsTypes unit) { Value = value; Unit = unit; }
    }
}
