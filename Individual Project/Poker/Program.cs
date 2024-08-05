using Poker;
class Program
{
	static void Main(string[] args)
	{
		var players = new List<Player>
		{
			new Player("Hello"),
			new Player("World")
		};

		var gameController = new GameController(players); 
		Console.ReadLine();
	}
	
}