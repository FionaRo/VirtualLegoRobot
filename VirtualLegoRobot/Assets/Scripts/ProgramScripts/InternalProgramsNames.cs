using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.ProgramScripts
{
    //TODO дописать имена
    public static class InternalProgramsNames
    {
        public static List<string> motorsNames = new List<string> {
            "MotorStop\\.vix",
            "MotorTime\\.vix",
            "MotorUnlimited\\.vix",
            "MotorDistance\\.vix",
            "MotorDistanceRotations\\.vix",
            "MoveStop\\.vix",
            "MoveUnlimited\\.vix",
            "MoveDistanceRotations\\.vix",
            "MoveTime\\.vix",
            "MoveDistance\\.vix",
            "MoveTankMode\\.vix",
            "MoveTankStop\\.vix",
            "MoveTankTime\\.vix",
            "MoveTankDistanceRotations\\.vix",
            "MoveTankDistance\\.vix"
            };
        public static List<string> sensorsNames = new List<string> {
            "ButtonValue\\.vix",
            "ButtonCompare\\.vix",
            "ColorValue\\.vix",
            "ColorReflectedIntensity\\.vix",
            "ColorAmbientIntensity\\.vix",
            "ColorCompare\\.vix",
            "ColorReflectedIntensityCompare\\.vix",
            "ColorAmbientIntensityCompare\\.vix",
            "ColorCalibrateMin\\.vix", //???
            "ColorCalibrateMax\\.vix", //???
            "ColorCalibrateDefault\\.vix", //???
            "IRProximity\\.vix",
            "IRSeeker\\.vix",
            "IRRemote\\.vix",
            "IRProximityCompare\\.vix",
            "IRTrackerCompareHeading\\.vix",
            "IRTrackerCompareProximity\\.vix",
            "IRRemoteCompare\\.vix",
            "RotationValue\\.vix",
            "RotationValueRotations\\.vix",
            "MotorSpeedSensor\\.vix",
            "RotationDegreesCompare\\.vix",
            "RotationRotationsCompare\\.vix",
            "MotorSpeedCompare\\.vix",
            "RotationReset\\.vix",
            "TimerValue\\.vix",
            "TimerCompare\\.vix",
            "TimerReset\\.vix",
            "TouchValue\\.vix",
            "TouchCompare\\.vix",
        };
        public static List<string> variablesNames = new List<string> {
            "X3\\.Lib:GlobalSetString",
            "X3\\.Lib:GlobalSetSingle",
            "X3\\.Lib:GlobalSetBoolean",
            "X3\\.Lib:GlobalSetNumericArray",
            "X3\\.Lib:GlobalSetBooleanArray",
            "X3\\.Lib:GlobalGetString",
            "X3\\.Lib:GlobalGetSingle",
            "X3\\.Lib:GlobalGetBoolean",
            "X3\\.Lib:GlobalGetNumericArray",
            "X3\\.Lib:GlobalGetBooleanArray",
            "ArrayBuild\\.vix",
            "ArrayBuildBoolean\\.vix",
            "ArrayReadAtIndex\\.vix",
            "ArrayReadAtIndexBoolean\\.vix",
            "ArrayWriteAtIndex\\.vix",
            "ArrayWriteAtIndexBoolean\\.vix",
            "ArrayGetSize\\.vix",
            "ArrayGetSizeBoolean\\.vix",
            "Boolean_And\\.vix",
            "Boolean_Or\\.vix",
            "Boolean_XOr\\.vix",
            "Boolean_Not\\.vix",
            "Arithmetic_Add\\.vix",
            "Arithmetic_Subtract\\.vix",
            "Arithmetic_Divide\\.vix",
            "Arithmetic_Multiply\\.vix",
            "Arithmetic_AbsoluteValue\\.vix",
            "Arithmetic_SquareRoot\\.vix",
            "Arithmetic_Power\\.vix",
            "Round_Nearest\\.vix",
            "Round_Up\\.vix",
            "Round_Down\\.vix",
            "Round_Truncate\\.vix",
            "Comparison_Equal\\.vix",
            "Comparison_NotEqual\\.vix",
            "Comparison_Greater\\.vix",
            "Comparison_GreaterEqual\\.vix",
            "Comparison_Less\\.vix",
            "Comparison_LessEqual\\.vix",
            "InsideRange\\.vix",
            "OutsideRange\\.vix",
            "OutsideRange\\.vix",
            "RandomSingle\\.vix",
            "RandomBoolean\\.vix",

        };
        public static List<string> displayNames = new List<string> { }; //не поддерживается
    }
}
