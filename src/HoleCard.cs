﻿using System;
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

            if (rank1Enum == Ranks.LowNumber || rank2Enum == Ranks.LowNumber)
            {
                return false;
            }

            return true;
        }

        private static Ranks ParseRank(string rank)
        {
            rank = rank == "10" ? "X" : rank;
            
            if (Enum.IsDefined(typeof(Ranks), rank))
            {
                return (Ranks) Enum.Parse(typeof(Ranks), rank);
            }

            return Ranks.LowNumber;
        }

        public static bool IsHighPair(Player player)
        {
            return player.Cards
                .Select(c => c.Rank)
                // .Where(IsHighCard)
                .Distinct()
                .Count() == 1;
        }

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