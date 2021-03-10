using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nancy.Simple
{
    public sealed class GameState
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

        [JsonProperty("players")] public List<Player> Players { get; set; }

        [JsonProperty("community_cards")] public List<Card> CommunityCards { get; set; }
    }
}