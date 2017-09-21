using System;

namespace KeesTalksTech.Assessments.Wehkamp.Store.Utilities.Math
{
    public static class Randomizer
    {
        private static readonly Random _randomizer = new Random();

        public static double GenerateRandomNumber(double minimum, double maximum, int? decimals = null)
        {
            var result = _randomizer.NextDouble() * (maximum - minimum) + minimum;

            if(decimals != null)
            {
                result = System.Math.Round(result, decimals.GetValueOrDefault());
            }

            return result;
        }

        public static int GenerateRandomNumber(int minimum, int maximum)
        {
            var result = GenerateRandomNumber(minimum, maximum, 0);

            return Convert.ToInt32(result);
        }

        public static int GenerateRandomNumber(uint minimum, uint maximum)
        {
            var result = GenerateRandomNumber(minimum, maximum, 0);

            return Convert.ToInt32(result);
        }
    }
}
