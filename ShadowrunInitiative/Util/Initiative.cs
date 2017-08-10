using System;
using ShadowrunInitiative.Util;
using ShadowrunInitiative.Core;

namespace ShadowrunInitiative
{
    public static class Initiative
    {      
        public static int RollInitiative(CombatSituation situation, Character character)
        {
            int ir = character.intuition + character.reaction;
            int id = character.intuition + character.dataProcessing;
            switch (situation)
            {
                case CombatSituation.PHYSICAL:
                    return ir + D6.d6();
                case CombatSituation.ASTRAL:
                    return (2 * character.intuition) + D6.d6(2);
                case CombatSituation.MATRIX:
                    switch (character.matrixLevel)
                    {
                        case MatrixLevel.AR:
                            return ir + D6.d6();
                        case MatrixLevel.COLD_SIM:
                            return id + D6.d6(3);
                        case MatrixLevel.HOT_SIM:
                            return id + D6.d6(4);
                        default:
                            throw new NotImplementedException();
                    }
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
