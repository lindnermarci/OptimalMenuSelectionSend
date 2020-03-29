using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalMenuSelection
{
    class Randomizer
    {
        private static Random randomizer = new Random();

        /// <summary>
        /// Inclusive, random value returned may include either upper or lower boundary
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int IntBetween(int lower, int upper)
        {
            return randomizer.Next(lower, upper);
        }

        /// <summary>
        /// Returns an int up to, but not including the parameter
        /// </summary>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int IntLessThan(int upper)
        {
            return randomizer.Next(upper);
        }

        public static double GetRandomNumberUpTo(double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum);
        }


        /// <summary>
        /// Returns a double >= 0 and less than 1.0
        /// </summary>
        /// <returns></returns>
        public static double GetDoubleFromZeroToOne()
        {
            return randomizer.NextDouble();
        }
    }
}
