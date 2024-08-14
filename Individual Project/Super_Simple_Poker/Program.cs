using SuperSimplePoker;
using NLog;
using System.Linq;
using System.Collections.Generic;

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
        deck.LoadFromJson(@".\Json\Cards.json");
        Table table = new Table();

        GameController gameController = new GameController(deck, table, logger);

        foreach (var player in players)
        {
            gameController.InitializePlayer(player, moneyPerPlayer);
        }

        List<PlayerGameInfo> playersInfo = gameController.GetPlayers();

        while (playersInfo.Count > 1)
        {
            // Assign blinds
            AssignBlinds(playersInfo, gameController);

            // Deal hole cards
            gameController.DealHoleCards(playersInfo);

            // Run betting rounds
            RunBettingRound(gameController, playersInfo, "Pre-Flop");
            gameController.DealCommunityCards(3); // Flop
            RunBettingRound(gameController, playersInfo, "Flop");
            gameController.DealCommunityCards(1); // Turn
            RunBettingRound(gameController, playersInfo, "Turn");
            gameController.DealCommunityCards(1); // River
            RunBettingRound(gameController, playersInfo, "River");

            // Showdown and Determine Round Winner
            if (playersInfo.Count(p => p.PlayerIngame) > 1)
            {
                AssignBestCombinations(gameController, playersInfo);
                AnnounceRoundWinner(gameController, playersInfo);
            }

            // Remove players without money
            gameController.RemovePlayersWithoutMoney();

            // Check for overall game winner
            if (playersInfo.Count == 1)
            {
                AnnounceGameWinner(playersInfo.First());
                break;
            }
        }
    }

    static void AssignBlinds(List<PlayerGameInfo> playersInfo, GameController gameController)
    {
        int smallBlindAmount = playersInfo[0].Money / 100;
        int bigBlindAmount = smallBlindAmount * 2;

        PlayerGameInfo smallBlindPlayer = playersInfo[0];
        PlayerGameInfo bigBlindPlayer = playersInfo[1];

        smallBlindPlayer.Bet = smallBlindAmount;
        bigBlindPlayer.Bet = bigBlindAmount;

        smallBlindPlayer.Money -= smallBlindAmount;
        bigBlindPlayer.Money -= bigBlindAmount;

        gameController.AddToPot(smallBlindAmount + bigBlindAmount);

        Display.PrintBlinds(smallBlindPlayer.Player.Name, bigBlindPlayer.Player.Name, smallBlindAmount, bigBlindAmount);
    }

    static void RunBettingRound(GameController gameController, List<PlayerGameInfo> playersInfo, string roundName)
    {
        Display.PrintRoundStart(roundName);
        int minimumBet = playersInfo.Max(p => p.Bet);
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

                    ExecutePlayerChoice(player, choice, ref minimumBet);
                    Display.PrintNewLine();
                }
            }
        }

        int collectedAmount = playersInfo.Sum(player => player.Bet);
        playersInfo.ForEach(player => player.Bet = 0);
        gameController.AddToPot(collectedAmount);
        Display.PrintPot(gameController.GetPot());
    }

    static bool AllPlayersReady(List<PlayerGameInfo> playersInfo, int minimumBet)
    {
        return playersInfo.All(player => player.Money == 0 || player.Bet >= minimumBet);
    }

    static void ExecutePlayerChoice(PlayerGameInfo player, char choice, ref int minimumBet)
    {
        if (choice == 'C' && player.Bet < minimumBet)
        {
            int callAmount = minimumBet - player.Bet;
            player.Bet += callAmount;
            player.Money -= callAmount;
            Display.PrintCallAmount(callAmount);
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
            if (allInMoney > minimumBet) minimumBet = player.Bet;
        }
        else if (choice == 'R')
        {
            int raiseAmount = Display.GetRaiseAmount();
            if (raiseAmount <= player.Money)
            {
                player.Bet += raiseAmount;
                player.Money -= raiseAmount;
                minimumBet = player.Bet;
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
            if (player.PlayerIngame)
            {
                List<Card> combinedHand = CardSort(player.UnsortedHand, gameController.GetCommunityCards());
                Display.PrintPlayerCards(player.Player.Name, combinedHand);
                player.HandEvaluator = new HandEvaluator(combinedHand);
                Display.PrintBestCombination(player.Player.Name, player.HandEvaluator.EvaluateHand());
            }
        }
    }

    static void AnnounceRoundWinner(GameController gameController, List<PlayerGameInfo> playersInfo)
    {
        var activePlayers = playersInfo.Where(p => p.PlayerIngame).ToList();
        activePlayers = activePlayers.OrderByDescending(player => player.HandEvaluator.HandValues.Combination).ToList();
        PlayerGameInfo winner = activePlayers.First();
        Display.PrintRoundWinner(winner.Player.Name, winner.HandEvaluator.HandValues.Combination);
        winner.Money += gameController.GetPot();
        Display.PrintNewLine();
    }

    static void AnnounceGameWinner(PlayerGameInfo winner)
    {
        Display.PrintUltimateWinner(winner.Player.Name, winner.Money);
    }

    static List<Card> CardSort(List<Card> playerCards, List<Card> communityCards)
    {
        List<Card> totalCards = new List<Card>(playerCards);
        totalCards.AddRange(communityCards);
        return totalCards.OrderBy(card => card.Rank).ToList();
    }
}
