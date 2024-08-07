namespace SuperSimplePoker;

  public static class Display
    {
        public static void PrintCommunityCards(List<Card> communityCards)
        {
            Console.WriteLine("Community cards:");
            for (int i = 0; i < communityCards.Count; i++)
            {
                Console.WriteLine($"Card {i + 1} : {communityCards[i].Rank} {communityCards[i].Suit}");
            }
            Console.WriteLine();
        }

        public static void PrintPlayerStatus(PlayerGameInfo player, int minimumBet, List<Card> communityCards)
        {
            Console.WriteLine($"The minimum bet is: {minimumBet}");
            Console.WriteLine($"You have bet {player.Bet} this round");
            Console.WriteLine($"{player.Player.Name}'s turn");
            Console.WriteLine($"Money left: {player.Money}");
            PrintPlayerCards(player.Player.Name, player.UnsortedHand);
            player.HandEvaluator = new HandEvaluator(player.UnsortedHand); // Evaluate based on player's hand only
            Console.WriteLine($"Your best combination as of now: {player.HandEvaluator.EvaluateHand()}");
        }

        public static void PrintPlayerCards(string playerName, List<Card> cards)
        {
            Console.WriteLine($"{playerName}'s total cards:");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"Card {i + 1} : {cards[i].Rank} {cards[i].Suit}");
            }
        }

        public static void PrintBestCombination(string playerName, HandRankingEnum bestCombination)
        {
            Console.WriteLine($"{playerName}'s best combination: {bestCombination}\n");
        }

        public static void PrintPot(int pot)
        {
            Console.WriteLine($"The pot is: {pot}\n");
        }

        public static void PrintRoundWinner(string playerName, HandRankingEnum combination)
        {
            Console.WriteLine($"Player {playerName} wins!!! - {combination}");
        }

        public static void PrintUltimateWinner(string playerName, int money)
        {
            Console.WriteLine($"{playerName} You Survied, ChihkenDinner !!");
            Console.WriteLine($"Total Money: {money}");
        }

        public static void PrintPlayerKickedOut(string playerName)
        {
            Console.WriteLine($"{playerName} Kiked -Run out of money.");
        }

        public static void PrintCallAmount(int callAmount)
        {
            Console.WriteLine($"You have called for {callAmount}");
        }

        public static void PrintCheck()
        {
            Console.WriteLine("Checking...");
        }

        public static void PrintAllIn(int allInMoney)
        {
            Console.WriteLine($"You go all in for {allInMoney}");
        }

        public static void PrintRaiseAmount(int raiseAmount)
        {
            Console.WriteLine($"You have raised by {raiseAmount}");
        }

        public static void PrintFold()
        {
            Console.WriteLine("Folding...");
        }

        public static void PrintNewLine()
        {
            Console.WriteLine();
        }

        public static char GetPlayerChoice(PlayerGameInfo player, int minimumBet)
        {
            if (player.Money < minimumBet) Console.WriteLine("Input 'A' - All in");
            else if (player.Bet < minimumBet) Console.WriteLine($"Input 'C' - Call for {minimumBet - player.Bet}");
            else Console.WriteLine("Input 'C' - Check");
            if (player.Money > minimumBet) Console.WriteLine("Input 'A' - All in");
            Console.WriteLine("Input 'R' - Raise");
            Console.WriteLine("Input 'F' - Fold");

            return Convert.ToChar(Console.ReadLine().ToUpper());
        }

        public static int GetRaiseAmount()
        {
            Console.Write("How much do you want to raise by? ");
            return Convert.ToInt32(Console.ReadLine());
        }
    }
    
    



