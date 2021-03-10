using System;

namespace Nancy.Simple
{
    public static class HoleCard
    {
        public static bool IsHigh(string rank1, string suit1, string rank2, string suit2)
        {
            int number;

            if (rank1 == rank2)
            {
                int.TryParse(rank1, out number);

                if (number > 6 || number < 0)
                {
                    return true;
                }

                return false;
            }

            var rank1Enum = ParseRank(rank1);
            var rank2Enum = ParseRank(rank2);

            if (rank1Enum == Ranks.LowNumber || rank2Enum == Ranks.LowNumber)
            {
                return false;
            }

            return true;
        }

        private static Ranks ParseRank(string suit1)
        {
            if (Enum.IsDefined(typeof(Ranks), suit1))
            {
                return (Ranks) Enum.Parse(typeof(Ranks), suit1);
            }

            return Ranks.LowNumber;
        }
    }
}