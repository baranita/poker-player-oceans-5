using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "V01";

		public static int BetRequest(JObject gameState)
		{
			try
			{
				var team = gameState["players"].Single(p => p.Name == "Oceans 5");
				var cards = team["hole_cards"].ToList();
				var cardOne = cards[0]["rank"];
				var cardTwo = cards[1]["rank"];

				if (cardOne == cardTwo)
				{
					return int.MaxValue;
				}
			}
			catch (Exception e)
			{
				Console.Error.WriteLine(e.Message);
			}
			
			return int.MaxValue;
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

