using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
    public static class PokerPlayer
    {
        public static readonly string VERSION = "V02";

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

            if (IsHeadsUp(gameState) && HoleCard.IsHigh(player))
            {
                return GetRaiseBet(gameState);
            }

            if (HoleCard.IsPair(player))
            {
                return GetRaiseBet(gameState);
            }

            return 0;
        }

        private static int GetRaiseBet(GameState gameState)
        {
            return gameState.CurrentBuyIn - gameState.Players[gameState.InAction].Bet + gameState.MinimumRaise;
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