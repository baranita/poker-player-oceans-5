﻿using System;
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

            Console.Error.WriteLine("Round: " + gameState.Round 
                                  + ", BetIndex: " + gameState.BetIndex 
                                  + ", Dealer: " + gameState.Dealer);

            if (IsHeadsUp(gameState))
            {
                Console.Error.WriteLine("Is heads up");
                if (HoleCard.IsHigh(player.Cards[0], player.Cards[1])
                    || HoleCard.AllCardsAreGood(player.Cards, gameState.CommunityCards))
                {
                    // return GetMinimumRaiseBetTimes(gameState, 3);
                    return player.Stack;
                }
            }
            else
            {
                Console.Error.WriteLine("Is no heads up");
                if (HoleCard.IsHigh(player.Cards[0], player.Cards[1]))
                {
                    // return GetMinimumRaiseBetTimes(gameState, 1);
                    return player.Stack;
                }
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
            var activePlayers = gameState.Players.Where(p => p.Status == "active").ToList();
            return activePlayers.Count == 2;
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