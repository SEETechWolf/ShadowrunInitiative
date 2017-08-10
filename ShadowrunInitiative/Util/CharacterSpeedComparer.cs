using System;
using System.Collections.Generic;
using ShadowrunInitiative.Core;

namespace ShadowrunInitiative.Util
{
    /// <summary>
    /// IComparer for comparing character speed.
    /// </summary>
    public class CharacterSpeedComparer : IComparer<Character>
    {
        private static Random s_Random = new Random();

        public int Compare(Character a, Character b)
        {
            if (!a.delay && b.delay)
                return -1;
            else if (a.delay && !b.delay)
                return 1;

            if (!a.seize && b.seize)
                return -1;
            else if (a.seize && !b.seize)
                return 1;

            if (a.Initiative < b.Initiative)
                return -1;
            else if (a.Initiative > b.Initiative)
                return 1;

            if (a.edge < b.edge)
                return -1;
            else if (a.edge > b.edge)
                return 1;

            if (a.reaction < b.reaction)
                return -1;
            else if (a.reaction > b.reaction)
                return 1;

            if (a.intuition < b.intuition)
                return -1;
            else if (a.intuition > b.intuition)
                return 1;

            return (s_Random.Next() % 2) * 2 - 1;
        }
    }
}
