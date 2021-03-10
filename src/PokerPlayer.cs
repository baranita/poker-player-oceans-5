using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "V04";

        private const string TeamName = "Oceans 5";

        public static int BetRequest(JObject gameState)
        {
            try
            {
                return RunStrategy(ParseGameState(gameState));
            }
            catch (Exception exception)
            {
                Console.Error.WriteLine(exception.Message);
            }

            return int.MaxValue;
        }

        public static int RunStrategy(GameState gameState)
        {
            var player = gameState.Players.Single(p => p.Name == TeamName);

            if (IsHeadsUp(gameState))
            {
                var allCards = player.Cards.Union(gameState.CommunityCards).ToList();
                if (HoleCard.IsHigh(player.Cards[0], player.Cards[1]) || HoleCard.AllCardsAreGood(player.Cards, gameState.CommunityCards))
                {
                    return GetMinimumRaiseBetTimes(gameState, 3);
                }
            }

            if (HoleCard.IsPair(player))
            {
                return GetMinimumRaiseBetTimes(gameState, 1);
            }

            return 0;
        }

        private static int GetMinimumRaiseBetTimes(GameState gameState, int times)
        {
            return gameState.CurrentBuyIn
                   - gameState.Players[gameState.InAction].Bet
                   + (times * gameState.MinimumRaise);
        }

        private static bool IsHeadsUp(GameState gameState)
        {
            return gameState.Players.Count(p => p.Status == "active") == 2;
        }

        public static void ShowDown(JObject gameState)
        {
            //TODO: Use this method to showdown
        }

        public static GameState ParseGameState(JObject gameState)
        {
            return gameState.ToObject<GameState>();
        }
    }
}