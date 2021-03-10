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
                return IsHighCard(rank1);
            }

            var rank1Enum = ParseRank(rank1);
            var rank2Enum = ParseRank(rank2);

            if ((int) rank1Enum < 10 || (int) rank2Enum < 10)
            {
                return false;
            }

            return true;
        }

        private static Ranks ParseRank(string rank)
        {
            if (Enum.IsDefined(typeof(Ranks), rank))
            {
                return (Ranks) Enum.Parse(typeof(Ranks), rank);
            }

            int number;
            if (int.TryParse(rank, out number))
            {
                return (Ranks) number;
            }

            return 0;
        }

        // public static bool IsHighPair(Player player)
        // {
        //     return player.Cards
        //         .Select(c => c.Rank)
        //         // .Where(IsHighCard)
        //         .Distinct()
        //         .Count() == 1;
        // }

        private static bool IsHighCard(string rank)
        {
            int number;
            int.TryParse(rank, out number);

            if (number >= 7 || number < 0)
            {
                return true;
            }

            return false;
        }

        public static bool AllCardsAreGood(IList<Card> playerCards, IList<Card> communityCards)
        {
            var allCards = playerCards.Union(communityCards).ToList();

            if (HasAtLeastOnePair(playerCards, communityCards)) return true;

            if (IsStraight(allCards) && !IsStraight(communityCards)) return true;

            if (IsFlush(allCards) && !IsFlush(communityCards)) return true;


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

        private static bool IsStraight(IList<Card> cards)
        {
            var orderedRanks = cards.Select(c => ParseRank(c.Rank)).OrderBy(r => r);
            var previousRank = (Ranks) 0;
            var count = 0;
            foreach (var rank in orderedRanks)
            {
                if ((int) rank - (int) previousRank == 1)
                {
                    count++;
                }

                previousRank = rank;
            }

            return count >= 5;
        }

        private static bool IsFlush(IList<Card> cards)
        {
            return cards.GroupBy(c => c.Suit).Any(g => g.Count() >= 5);
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