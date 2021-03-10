using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    public sealed class Player : IEquatable<Player>
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("stack")] public int Stack { get; set; }
        [JsonProperty("bet")] public int Bet { get; set; }
        [JsonProperty("hole_cards")] public List<Card> Cards { get; set; } = new List<Card>();

        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, " +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(Status)}: {Status}, " +
                   $"{nameof(Stack)}: {Stack}, " +
                   $"{nameof(Bet)}: {Bet}, " +
                   $"{nameof(Cards)}: [{(Cards == null ? string.Empty : string.Join(", ", Cards))}]";
        }

        public bool Equals(Player other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && Name == other.Name && Status == other.Status && Stack == other.Stack &&
                   Bet == other.Bet && Cards.SequenceEqual(other.Cards);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is Player other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Status != null ? Status.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Stack;
                hashCode = (hashCode * 397) ^ Bet;
                hashCode = (hashCode * 397) ^ (Cards != null ? Cards.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}