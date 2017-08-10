using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowrunInitiative.Util
{
    class D6
    {
        private static Random m_Random = new Random();

        public static int d6()
        {
            return m_Random.Next(6) + 1;
        }

        public static int d6(int qty)
        {
            int roll = qty;
            for (int c = 0; c < qty; c++)
                roll += m_Random.Next(6);
            return roll;
        }
    }
}
