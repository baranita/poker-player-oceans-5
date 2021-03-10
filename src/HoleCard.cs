using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public static class HoleCard
    {
        public static bool IsHigh(Card firstCard, Card secondCard)
        {
            var rank1 = firstCard.Rank;
            var suit1 = firstCard.Suit;
            var rank2 = secondCard.Rank;
            var suit2 = secondCard.Suit;

            if (rank1 == rank2)
            {
                int number;
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

        public static bool IsPair(Player player)
        {
            return player.Cards.Select(c => c.Rank).Distinct().Count() == 1;
        }

        public static bool AllCardsAreGood(IList<Card> playerCards, IList<Card> communityCards)
        {
            if (HasAtLeastOnePair(playerCards, communityCards)) return true;

            // var sameSuit = allCards.GroupBy(c => c.Suit).ToList();
            // if (sameSuit.Any(g => g.Count() >= 5))
            // {
            //     return true;
            // }
            // if (sameSuit.Any(g => g.Count() == 4) && allCards.Count <= 5)
            // {
            //     return true;
            // }
            // if (sameSuit.Any(g => g.Count() == 2) && allCards.Count == 2 && )
            // {
            //     return true;
            // }

            return false;
        }

        private static bool HasAtLeastOnePair(IList<Card> playerCards, IList<Card> communityCards)
        {
            if (playerCards.Select(c => c.Rank).Intersect(communityCards.Select(c => c.Rank)).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}