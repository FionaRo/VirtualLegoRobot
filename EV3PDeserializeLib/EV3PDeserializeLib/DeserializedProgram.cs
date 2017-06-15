using System.Collections.Generic;
using EV3PDeserializeLib.Interfaces;

namespace EV3PDeserializeLib
{
    public struct DeserializedProgram
    {
        public Dictionary<string, IBlock> WiresDictionary { get; set; }
        public Dictionary<string, ConfigurableFlatCaseStructure> Switch { get; set; }
        public Queue<Wire> TurnRunning { get; set; }
        public Dictionary<string, ConfigurableMegaAccessor> PersonalData { get; set; }
    }
}
