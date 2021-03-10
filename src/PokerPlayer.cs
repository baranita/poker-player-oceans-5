using Newtonsoft.Json.Linq;

namespace Nancy.Simple
{
	public static class PokerPlayer
	{
		public static readonly string VERSION = "V02";

		private const string TeamName = "Oceans 5";

		public static int BetRequest(JObject gameState)
		{
			var currentPlayer = gameState["players"][TeamName];
			return currentPlayer["stack"].Value<int>();
		}

		public static void ShowDown(JObject gameState)
		{
			//TODO: Use this method to showdown
		}
	}
}

