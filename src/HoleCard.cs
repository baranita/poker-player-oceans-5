using System;
using System.Linq;

namespace Nancy.Simple
{
    public static class HoleCard
    {
        public static bool IsHigh(Player player)
        {
            var rank1 = player.Cards[0].Rank;
            var suit1 = player.Cards[0].Suit;
            var rank2 = player.Cards[1].Rank;
            var suit2 = player.Cards[1].Suit;

            if (rank1 == rank2)
            {
                int.TryParse(rank1, out var number);

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

        public static bool IsPair(Player player)
        {
            return player.Cards.Select(c => c.Rank).Distinct().Count() == 1;
        }
    }
}