using Super_Simple_Poker;


	 class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("How many players?: ");
            int playerCount = Int16.Parse(Console.ReadLine());

            Console.WriteLine("How much money per player?: ");
            int moneyPerPlayer = Int32.Parse(Console.ReadLine());

            DeckOfCards deck = new DeckOfCards();
            deck.LoadFromJson("./Json/Cards.json");

            GameController gameController = new GameController(playerCount, moneyPerPlayer, deck);
            gameController.StartGame();
        }
    }


