using System;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    public sealed class Card : IEquatable<Card>
    {
        [JsonProperty("rank")] public string Rank { get; set; }
        [JsonProperty("suit")] public string Suit { get; set; }

        public bool Equals(Card other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Rank == other.Rank && Suit == other.Suit;
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Card other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Rank != null ? Rank.GetHashCode() : 0) * 397) ^ (Suit != null ? Suit.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return $"{nameof(Rank)}: {Rank}, " +
                   $"{nameof(Suit)}: {Suit}";
        }
    }
}