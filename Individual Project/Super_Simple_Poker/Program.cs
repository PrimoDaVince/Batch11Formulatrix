using SuperSimplePoker;
using NLog;

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        Console.WriteLine("How many players?: ");
        int playerCount = Int16.Parse(Console.ReadLine());

        List<Player> players = new List<Player>();
        for (int i = 0; i < playerCount; i++)
        {
            Console.WriteLine($"Enter name for Player {i + 1}: ");
            string playerName = Console.ReadLine();
            players.Add(new Player(i + 1, playerName));
        }

        Console.WriteLine("How much money per player?: ");
        int moneyPerPlayer = Int32.Parse(Console.ReadLine());

        DeckOfCards deck = new DeckOfCards();
        deck.LoadFromJson(@"C:\Path\To\Cards.json");
        Table table = new Table();

        GameController gameController = new GameController(deck, table, logger);

        foreach (var player in players)
        {
            gameController.InitializePlayer(player, moneyPerPlayer);
        }

        // Ask user if they want to print all cards
        // Console.WriteLine("Do you want to print all cards in the deck? (yes/no): ");
        // string choice = Console.ReadLine().ToLower();
        // bool printAllCards = choice == "yes";

        // if (printAllCards)
        // {
        //     List<Card> allCards = gameController.GetAllCards();
        //     Console.WriteLine("All Cards in the Deck:");
        //     foreach (var card in allCards)
        //     {
        //         Console.WriteLine($"Card ID: {card.IdCard}, Rank: {card.Rank}, Suit: {card.Suit}");
        //     }
        // }

        // Game logic moved to Program.cs
        List<PlayerGameInfo> playersInfo = gameController.GetPlayers();

        while (playersInfo.Count > 1)
        {
            int minimumBet = playersInfo[0].Money / 50;

            gameController.ClearCommunityCards();
            foreach (var playerInfo in playersInfo)
            {
                playerInfo.Bet = 0;
            }

            gameController.ClearPot();

            // Run game rounds
            RunGameRounds(gameController, playersInfo, minimumBet);

            // Assign best combinations
            AssignBestCombinations(gameController, playersInfo);

            // Announce winner
            AnnounceWinner(gameController, playersInfo);

            // Remove players without money
            gameController.RemovePlayersWithoutMoney();
        }
    }

    static void RunGameRounds(GameController gameController, List<PlayerGameInfo> playersInfo, int minimumBet)
    {
        for (int i = 0; i < 4; i++) // 4 betting rounds (pre-flop, flop, turn, river)
        {
            int roundPot = PlayRound(gameController, playersInfo, minimumBet);
            gameController.AddToPot(roundPot);

            if (i == 0) gameController.DealCommunityCards(3); // Flop
            else if (i < 3) gameController.DealCommunityCards(1); // Turn and River

            Display.PrintCommunityCards(gameController.GetCommunityCards());
            Display.PrintPot(gameController.GetPot());
        }
    }

    static int PlayRound(GameController gameController, List<PlayerGameInfo> playersInfo, int minimumBet)
    {
        bool roundComplete = false;

        while (!roundComplete)
        {
            foreach (var player in playersInfo)
            {
                if (AllPlayersReady(playersInfo, minimumBet))
                {
                    roundComplete = true;
                    break;
                }

                if (player.PlayerIngame)
                {
                    Display.PrintPlayerStatus(player, minimumBet, gameController.GetCommunityCards());
                    char choice = Display.GetPlayerChoice(player, minimumBet);

                    ExecutePlayerChoice(player, choice, minimumBet);
                    Display.PrintNewLine();
                }
            }
        }

        int collectedAmount = playersInfo.Sum(player => player.Bet);
        playersInfo.ForEach(player => player.Bet = 0);
        return collectedAmount;
    }

    static bool AllPlayersReady(List<PlayerGameInfo> playersInfo, int minimumBet)
    {
        return playersInfo.All(player => player.Money == 0 || player.Bet >= minimumBet);
    }

    static void ExecutePlayerChoice(PlayerGameInfo player, char choice, int minimumBet)
    {
        if (choice == 'C' && player.Bet < minimumBet)
        {
            int callAmount = minimumBet - player.Bet;
            Display.PrintCallAmount(callAmount);
            player.Bet += callAmount;
            player.Money -= callAmount;
        }
        else if (choice == 'C' && player.Bet == minimumBet)
        {
            Display.PrintCheck();
        }
        else if (choice == 'A')
        {
            int allInMoney = player.Money;
            player.Money = 0;
            player.Bet += allInMoney;
            Display.PrintAllIn(allInMoney);
            if (allInMoney > minimumBet) minimumBet += allInMoney;
        }
        else if (choice == 'R')
        {
            int raiseAmount = Display.GetRaiseAmount();
            if (raiseAmount <= player.Money)
            {
                player.Bet += raiseAmount;
                player.Money -= raiseAmount;
                player.Bet += minimumBet;
                player.Money -= minimumBet;
                minimumBet += raiseAmount;
                Display.PrintRaiseAmount(raiseAmount);
            }
        }
        else if (choice == 'F')
        {
            Display.PrintFold();
            player.PlayerIngame = false;
        }
    }

    static void AssignBestCombinations(GameController gameController, List<PlayerGameInfo> playersInfo)
    {
        foreach (var player in playersInfo)
        {
            List<Card> combinedHand = CardSort(player.UnsortedHand, gameController.GetCommunityCards());
            Display.PrintPlayerCards(player.Player.Name, combinedHand);
            player.HandEvaluator = new HandEvaluator(combinedHand);
            Display.PrintBestCombination(player.Player.Name, player.HandEvaluator.EvaluateHand());
        }
    }

    static void AnnounceWinner(GameController gameController, List<PlayerGameInfo> playersInfo)
    {
        playersInfo = playersInfo.OrderBy(player => player.HandEvaluator.HandValues.Combination).ToList();
        PlayerGameInfo winner = playersInfo.Last();
        Display.PrintRoundWinner(winner.Player.Name, winner.HandEvaluator.HandValues.Combination);
        winner.Money += gameController.GetPot();
        Display.PrintNewLine();
    }

    static List<Card> CardSort(List<Card> playerCards, List<Card> communityCards)
    {
        List<Card> totalCards = new List<Card>(playerCards);
        totalCards.AddRange(communityCards);
        return totalCards.OrderBy(card => card.Rank).ToList();
    }
}