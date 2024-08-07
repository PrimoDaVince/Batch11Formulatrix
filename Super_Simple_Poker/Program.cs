using Super_Simple_Poker;
class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("How many players?: ");
		int playerCount = Int16.Parse(Console.ReadLine());

		List<string> playerNames = new List<string>();
		for (int i = 0; i < playerCount; i++)
		{
			Console.WriteLine($"Enter name for Player {i + 1}: ");
			string playerName = Console.ReadLine();
			playerNames.Add(playerName);
		}

		Console.WriteLine("How much money per player?: ");
		int moneyPerPlayer = Int32.Parse(Console.ReadLine());

		DeckOfCards deck = new DeckOfCards();
		deck.LoadFromJson(@"C:\Users\ACER\Desktop\Basic-Terminal-Poker\Super_Simple_Poker\Json\Cards.Json");

		GameController gameController = new GameController(playerNames, moneyPerPlayer, deck);
		gameController.StartGame();
	}
}






