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
                Console.Error.WriteLine(exception.StackTrace);
            }

            return 0;
        }

        public static int RunStrategy(GameState gameState)
        {
            var player = gameState.Players.Single(p => p.Name == TeamName);

            var betRound = gameState.BetIndex / gameState.Players.Count;

            if (player.Stack < 300 && gameState.Round % 2 == 0)
            {
                return player.Stack;
            }

            if(gameState.Round < 20)
            {
                return 0;
            }

            var isHeadsUp = IsHeadsUp(gameState);
            var shouldPlayMoreAggressive = ShouldPlayMoreAggressive(gameState, player);
            if (IsFirstBet(gameState))
            {
                if (HoleCard.IsHigh(player.Cards[0], player.Cards[1], shouldPlayMoreAggressive, isHeadsUp))
                {
                    return betRound <= 1
                        ? GetMinimumRaiseBetTimes(gameState, player.Bet, 1)
                        : GetMinimumRaiseBetTimes(gameState, player.Bet, 0);
                }
            }
            else
            {
                if (HoleCard.AllCardsAreGood(player.Cards, gameState.CommunityCards))
                {
                    return betRound <= 1
                        ? GetMinimumRaiseBetTimes(gameState, player.Bet, 1)
                        : GetMinimumRaiseBetTimes(gameState, player.Bet, 0);
                }
            }

            return 0;
        }

        private static bool ShouldPlayMoreAggressive(GameState gameState, Player player)
        {
            return player.Stack / 4 > gameState.SmallBlind;
        }

        private static bool IsFirstBet(GameState gameState)
        {
            return !gameState.CommunityCards.Any();
        }

        private static int GetMinimumRaiseBetTimes(GameState gameState, int currentBet, int times)
        {
            return gameState.CurrentBuyIn - currentBet + (gameState.MinimumRaise * times);
        }

        private static bool IsHeadsUp(GameState gameState)
        {
            var activePlayers = gameState.Players.Where(p => p.Status == "active").ToList();
            var isHeadsUp = activePlayers.Count == 2;
            Console.Error.WriteLine("IsHeadsUp: " + isHeadsUp
                                                  + ", Active: " + activePlayers.Count
                                                  + ", Players: " + gameState.Players.Count);
            return isHeadsUp;
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