using System;
using System.Collections.Generic;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            Display display = new Display();
            List<PlayerData> playerDataList = new List<PlayerData>();

            // Get player names from user input
            for (int i = 1; i <= 3; i++)
            {
                string playerName = display.GetPlayerName(i);
                playerDataList.Add(new PlayerData(new Player(playerName), 1000, 500));
            }
            // Path to the JSON file containing card data
            string cardJsonFilePath = @"C:\Users\Batch 11\Desktop\BootcampAkmal\Individual Project\PokerGPT\Json\Cards.json";

            // Initialize deck
            Deck deck = new Deck(cardJsonFilePath);
            // Initialize table with a minimum buy-in
            Table table = new Table(1, 100);

           

            // Initialize GameController
            GameController gameController = new GameController(playerDataList, table, deck, cardJsonFilePath);

            // Start the game
            gameController.StartGame();
        }
    }
}
