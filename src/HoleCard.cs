using System;
using System.Collections.Generic;
using System.Linq;

namespace Nancy.Simple
{
    public static class HoleCard
    {
        public static bool IsHigh(Card firstCard, Card secondCard, bool shouldPlayMoreAggressive, bool isHeadsUp)
        {
            var rank1 = firstCard.Rank;
            var suit1 = firstCard.Suit;
            var rank2 = secondCard.Rank;
            var suit2 = secondCard.Suit;

            if (rank1 == rank2 || suit1 == suit2)
            {
                return IsHighCard(rank1) && IsHighCard(rank2);
            }

            var rank1Enum = ParseRank(rank1);
            var rank2Enum = ParseRank(rank2);
            if ((isHeadsUp && (int) rank1Enum < 10 || (int) rank2Enum < 10)
                || ((int) rank1Enum < 10 && (int) rank2Enum < 10))
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

        private static bool IsHighCard(string rank)
        {
            int number;
            int.TryParse(rank, out number);

            if (number >= 9 || number < 0)
            {
                return true;
            }

            return false;
        }

        public static int GetScore(IList<Card> playerCards, IList<Card> communityCards)
        {
            var allCards = playerCards.Union(communityCards).ToList();

            if (HasAtLeastOnePair(playerCards, communityCards)) return 1;

            if (IsStraight(allCards) && !IsStraight(communityCards)) return 3;

            if (IsFlush(allCards) && !IsFlush(communityCards)) return 4;

            if (IsFullHouse(allCards) && !IsFullHouse(communityCards)) return 5;


            // if (sameSuit.Any(g => g.Count() == 4) && allCards.Count <= 5)
            // {
            //     return true;
            // }
            // if (sameSuit.Any(g => g.Count() == 2) && allCards.Count == 2 && )
            // {
            //     return true;
            // }

            return 0;
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

        private static bool IsFullHouse(IList<Card> cards)
        {
            var sameCardCounts = cards
                .GroupBy(c => ParseRank(c.Rank))
                .Select(g => g.Count())
                .Where(x => x >= 2)
                .ToList();

            return sameCardCounts.Count >= 2 && sameCardCounts.Any(x => x >= 3);
        }

        private static bool IsFlush(IList<Card> cards)
        {
            return cards.GroupBy(c => c.Suit).Any(g => g.Count() >= 5);
        }

        private static bool HasAtLeastOnePair(IList<Card> playerCards, IList<Card> communityCards)
        {
            var pairsWithCommunityCards = playerCards
                .Select(c => ParseRank(c.Rank))
                .Intersect(communityCards.Select(c => ParseRank(c.Rank)))
                .ToList();
            if (pairsWithCommunityCards.Any() && pairsWithCommunityCards.Max() >= Ranks.Eight)
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