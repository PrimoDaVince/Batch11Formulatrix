using SuperSimplePoker;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        int playerCount = GetValidInput("How many players?", input => int.TryParse(input, out var count) && count > 1, "Please enter a valid number greater than 1 for players.");
        List<Player> players = InitializePlayers(playerCount);
        int moneyPerPlayer = GetValidInput("How much money per player?", input => int.TryParse(input, out var money) && money > 0, "Please enter a valid positive amount of money.");

        GameController gameController = InitializeGame(players, moneyPerPlayer, logger);

        while (true)
        {
            int smallBlind = 10;
            int bigBlind = 20;

            gameController.RotateDealer();
            gameController.PostBlinds(smallBlind, bigBlind);
            gameController.DealHoleCards();

            if (CheckFoldVictory(gameController)) continue;

            RunBettingRounds(gameController, "Pre-Flop");
            if (CheckFoldVictory(gameController)) continue;

            gameController.DealCommunityCards(3);  // Flop
            RunBettingRounds(gameController, "Flop");
            if (CheckFoldVictory(gameController)) continue;

            gameController.DealCommunityCards(1);  // Turn
            RunBettingRounds(gameController, "Turn");
            if (CheckFoldVictory(gameController)) continue;

            gameController.DealCommunityCards(1);  // River
            RunBettingRounds(gameController, "River");
            if (CheckFoldVictory(gameController)) continue;

            var winner = gameController.DetermineWinner();
            Display.PrintRoundWinner(winner.Player.Name, winner.HandEvaluator.HandValues.Combination.ToString());

            gameController.ClearPot();
            gameController.RemovePlayersWithoutMoney();

            if (gameController.IsOnlyOnePlayerRemaining())
            {
                var remainingPlayer = gameController.GetPlayerStatuses().First(p => p.PlayerIngame);
                Display.PrintUltimateWinner(remainingPlayer.Name, remainingPlayer.Money);
                break;
            }

            HandlePlayerJoinQuit(gameController);
        }
    }

    static void HandlePlayerJoinQuit(GameController gameController)
    {
        Display.PrintPlayerJoinQuitOptions();
        string input = Console.ReadLine().ToUpper();

        if (input == "Q")
        {
            Console.WriteLine("Enter the name of the player who wants to quit:");
            string name = Console.ReadLine();
            gameController.PlayerQuit(name);
        }
        else if (input == "J")
        {
            Console.WriteLine("Enter the name of the new player:");
            string name = Console.ReadLine();
            int money = GetValidInput("How much money will the new player bring?", input => int.TryParse(input, out var money) && money > 0, "Please enter a valid positive amount of money.");
            gameController.PlayerJoin(new Player(gameController.GetPlayerCount() + 1, name), money);
        }
    }

    static bool CheckFoldVictory(GameController gameController)
    {
        if (gameController.IsOnlyOnePlayerRemaining())
        {
            var winner = gameController.GetPlayerStatuses().First(p => p.PlayerIngame);
            Display.PrintRoundWinner(winner.Name, "by default (everyone else folded)");
            gameController.ClearPot();
            gameController.RemovePlayersWithoutMoney();
            return true;
        }
        return false;
    }

   static void RunBettingRounds(GameController gameController, string roundName)
{
    Display.PrintRoundStart(roundName);

    var playerStatuses = gameController.GetPlayerStatuses();
    int minimumBet = playerStatuses.Max(p => p.Bet);

    foreach (var player in playerStatuses)
    {
        if (!player.PlayerIngame) continue;

        Display.PrintPlayerStatus(player.Name, player.Money, player.Bet, minimumBet);
        PrintPlayerAndCommunityCards(player, gameController.GetCommunityCards());

        bool canCheck = player.Bet == minimumBet;
        bool canCall = player.Bet < minimumBet;
        bool canRaise = player.Money > minimumBet;

        char choice = GetValidPlayerChoice(canCheck, canCall, canRaise);
        ProcessPlayerChoice(gameController, player, choice, minimumBet);

        if (CheckFoldVictory(gameController)) return;
    }
}

    static void PrintPlayerAndCommunityCards((string Name, int Money, int Bet, bool PlayerIngame, List<string> Cards) player, List<string> communityCards)
    {
        int cardNumber = 1;
        foreach (var cardDescription in player.Cards)
        {
            Display.PrintPlayerCard(cardDescription, cardNumber++);
        }

        cardNumber = 1;
        foreach (var cardDescription in communityCards)
        {
            Display.PrintCommunityCard(cardDescription, cardNumber++);
        }
    }

    static char GetValidPlayerChoice(bool canCheck, bool canCall, bool canRaise)
{
    while (true)
    {
        Display.PrintPlayerOptions(BuildOptions(canCheck, canCall, canRaise));
        Display.PromptPlayerChoice();

        string input = Console.ReadLine().ToUpper().Trim();

        if (input.Length == 1 && ValidateChoice(input[0], canCheck, canCall, canRaise))
        {
            return input[0];  // Valid input
        }

        Console.WriteLine("Invalid choice. Please enter a valid option: K (Check), C (Call), R (Raise), A (All-In), or F (Fold).");
    }
}

   static bool ValidateChoice(char choice, bool canCheck, bool canCall, bool canRaise)
{
    if (choice == 'C' && !canCall) return false;
    if (choice == 'K' && !canCheck) return false;
    if (choice == 'R' && !canRaise) return false;
    return true;
}

static string BuildOptions(bool canCheck, bool canCall, bool canRaise)
{
    string options = "Your options:\n";
    options += canCheck ? "K - Check\n" : "";
    options += canCall ? "C - Call\n" : "";
    if (canRaise) options += "R - Raise\n";
    options += "A - All In\nF - Fold";
    return options;
}

static void ProcessPlayerChoice(GameController gameController, (string Name, int Money, int Bet, bool PlayerIngame, List<string> Cards) player, char choice, int minimumBet)
{
    switch (choice)
    {
        case 'C':
            int callAmount = minimumBet - player.Bet;
            gameController.PlayerCall(player.Name, callAmount);
            Display.PrintCallAmount(callAmount);
            break;
        case 'R':
            Display.PromptRaiseAmount();
            int raiseAmount = int.Parse(Console.ReadLine());
            gameController.PlayerRaise(player.Name, raiseAmount);
            Display.PrintRaiseAmount(raiseAmount);
            break;
        case 'F':
            gameController.PlayerFold(player.Name);
            Display.PrintFold();
            break;
        case 'A':
            gameController.PlayerAllIn(player.Name);
            Display.PrintAllIn(player.Money);
            break;
        case 'K':
            Display.PrintCheck();
            break;
        default:
            Console.WriteLine("Invalid choice.");
            break;
    }

    Display.PrintNewLine();
}

    static GameController InitializeGame(List<Player> players, int moneyPerPlayer, ILogger logger)
    {
        DeckOfCards deck = new DeckOfCards();
        deck.LoadFromJson(@".\Json\Cards.json");
        Table table = new Table();
        GameController gameController = new GameController(deck, table, logger);

        foreach (var player in players)
        {
            gameController.InitializePlayer(player, moneyPerPlayer);
        }

        return gameController;
    }

    static List<Player> InitializePlayers(int playerCount)
    {
        var players = new List<Player>();
        for (int i = 0; i < playerCount; i++)
        {
            Console.WriteLine($"Enter name for Player {i + 1}: ");
            players.Add(new Player(i + 1, Console.ReadLine()));
        }
        return players;
    }

    static int GetValidInput(string prompt, Func<string, bool> validation, string errorMessage)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();
            if (validation(input))
            {
                return int.Parse(input);
            }
            Console.WriteLine(errorMessage);
        }
    }
}
