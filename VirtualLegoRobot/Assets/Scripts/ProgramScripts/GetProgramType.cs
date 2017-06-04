using Assets.Scripts.Data;

namespace Assets.Scripts.ProgramScripts
{
    public static class GetProgramType
    {
        public static BlockTypes GetBlockType(string blockName)
        {
            if (InternalProgramNames.MotorsNames.Contains(blockName))
                return BlockTypes.LargeMotor;
            //TODO возвращение типа блока
            return 0;
        }

        public static SensorTypes GetSensorType(string blockName)
        {
            //TODO возвращение типа сенсора
            return 0;
        }

        public static DisplayTypes GetDisplayType(string blockName)
        {
            //TODO возвращения типа рисования
            return 0;
        }
    }
}
