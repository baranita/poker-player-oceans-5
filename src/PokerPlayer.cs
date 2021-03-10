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
				var game = ParseGameState(gameState);
				var player = game.Players.Single(p => p.Name == TeamName);

				if (AreCardsOfSameRank(player))
				{
					return player.Stack;
				}
				else
				{
					return 0;
				}
			}
			catch (Exception exception)
			{
				Console.Error.WriteLine(exception.Message);
			}
			
			return int.MaxValue;
		}

		private static bool AreCardsOfSameRank(Player player)
		{
			return player.Cards.Select(c => c.Rank).Distinct().Count() == 1;
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

