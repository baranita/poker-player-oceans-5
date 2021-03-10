using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    public sealed class Player
    {
        [JsonProperty("id")] public int Id { get; set; }
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("status")] public string Status { get; set; }
        [JsonProperty("stack")] public int Stack { get; set; }
        [JsonProperty("bet")] public int Bet { get; set; }
        [JsonProperty("hole_cards")] public List<Card> Cards { get; set; } = new List<Card>();
    }
}