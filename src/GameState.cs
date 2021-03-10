using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    public sealed class GameState : IEquatable<GameState>
    {
        [JsonProperty("round")] public int Round { get; set; }

        [JsonProperty("bet_index")] public int BetIndex { get; set; }

        [JsonProperty("small_blind")] public int SmallBlind { get; set; }

        [JsonProperty("current_buy_in")] public int CurrentBuyIn { get; set; }

        [JsonProperty("pot")] public int Pot { get; set; }

        [JsonProperty("minimum_raise")] public int MinimumRaise { get; set; }

        [JsonProperty("dealer")] public int Dealer { get; set; }

        [JsonProperty("orbits")] public int Orbits { get; set; }

        [JsonProperty("in_action")] public int InAction { get; set; }

        [JsonProperty("players")] public List<Player> Players { get; set; } = new List<Player>();

        [JsonProperty("community_cards")] public List<Card> CommunityCards { get; set; } = new List<Card>();

        public bool Equals(GameState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Round == other.Round && BetIndex == other.BetIndex && SmallBlind == other.SmallBlind &&
                   CurrentBuyIn == other.CurrentBuyIn && Pot == other.Pot && MinimumRaise == other.MinimumRaise &&
                   Dealer == other.Dealer && Orbits == other.Orbits && InAction == other.InAction &&
                   Players.SequenceEqual(other.Players) && CommunityCards.SequenceEqual(other.CommunityCards);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is GameState other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Round;
                hashCode = (hashCode * 397) ^ BetIndex;
                hashCode = (hashCode * 397) ^ SmallBlind;
                hashCode = (hashCode * 397) ^ CurrentBuyIn;
                hashCode = (hashCode * 397) ^ Pot;
                hashCode = (hashCode * 397) ^ MinimumRaise;
                hashCode = (hashCode * 397) ^ Dealer;
                hashCode = (hashCode * 397) ^ Orbits;
                hashCode = (hashCode * 397) ^ InAction;
                hashCode = (hashCode * 397) ^ (Players != null ? Players.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CommunityCards != null ? CommunityCards.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return
                $"{nameof(Round)}: {Round}, " +
                $"{nameof(BetIndex)}: {BetIndex}, " +
                $"{nameof(SmallBlind)}: {SmallBlind}, " +
                $"{nameof(CurrentBuyIn)}: {CurrentBuyIn}, " +
                $"{nameof(Pot)}: {Pot}, " +
                $"{nameof(MinimumRaise)}: {MinimumRaise}, " +
                $"{nameof(Dealer)}: {Dealer}, " +
                $"{nameof(Orbits)}: {Orbits}, " +
                $"{nameof(InAction)}: {InAction}, " +
                $"{nameof(Players)}: [{(Players == null ? string.Empty : string.Join (", ", Players))}], " +
                $"{nameof(CommunityCards)}: [{(CommunityCards == null ? string.Empty : string.Join (", ", CommunityCards))}]";
        }
    }
}