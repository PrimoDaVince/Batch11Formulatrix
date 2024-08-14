namespace SuperSimplePoker
{
    public static class Display
    {
        public static void PrintCommunityCards(List<Card> communityCards)
        {
            Console.WriteLine("Community Cards:");
            for (int i = 0; i < communityCards.Count; i++)
            {
                Console.WriteLine($"Card {i + 1}: {communityCards[i].Rank} of {communityCards[i].Suit}");
            }
            Console.WriteLine();
        }

        public static void PrintPlayerStatus(PlayerGameInfo player, int minimumBet, List<Card> communityCards)
        {
            Console.WriteLine($"--- {player.Player.Name}'s Turn ---");
            Console.WriteLine($"Money left: {player.Money}");
            Console.WriteLine($"Current Bet: {player.Bet}");
            Console.WriteLine($"Minimum Bet to Call: {minimumBet}");

            Console.WriteLine("\nYour Cards:");
            PrintPlayerCards(player.Player.Name, player.UnsortedHand);

            Console.WriteLine("\nCommunity Cards:");
            PrintCommunityCards(communityCards);

            player.HandEvaluator = new HandEvaluator(player.UnsortedHand.Concat(communityCards).ToList());
            Console.WriteLine($"Your Best Hand: {player.HandEvaluator.EvaluateHand()}");
            Console.WriteLine("------------------------------\n");
        }

        public static void PrintPlayerCards(string playerName, List<Card> cards)
        {
            Console.WriteLine($"{playerName}'s Cards:");
            for (int i = 0; i < cards.Count; i++)
            {
                Console.WriteLine($"Card {i + 1}: {cards[i].Rank} of {cards[i].Suit}");
            }
        }

        public static void PrintBestCombination(string playerName, HandRankingEnum bestCombination)
        {
            Console.WriteLine($"{playerName}'s Best Hand: {bestCombination}\n");
        }

        public static void PrintPot(int pot)
        {
            Console.WriteLine($"Current Pot: {pot}\n");
        }

        public static void PrintRoundWinner(string playerName, HandRankingEnum combination)
        {
            Console.WriteLine($"*** {playerName} wins the round with {combination}! ***\n");
        }

        public static void PrintUltimateWinner(string playerName, int money)
        {
            Console.WriteLine($"### {playerName} is the Ultimate Winner! ###");
            Console.WriteLine($"Total Money Won: {money}\n");
        }

        public static void PrintPlayerKickedOut(string playerName)
        {
            Console.WriteLine($"--- {playerName} has been eliminated from the game. ---");
        }

        public static void PrintCallAmount(int callAmount)
        {
            Console.WriteLine($"You called for {callAmount} chips.");
        }

        public static void PrintCheck()
        {
            Console.WriteLine("You checked.");
        }

        public static void PrintAllIn(int allInMoney)
        {
            Console.WriteLine($"You went all in with {allInMoney} chips!");
        }

        public static void PrintRaiseAmount(int raiseAmount)
        {
            Console.WriteLine($"You raised the bet by {raiseAmount} chips.");
        }

        public static void PrintFold()
        {
            Console.WriteLine("You folded.");
        }

        public static void PrintNewLine()
        {
            Console.WriteLine();
        }

        public static char GetPlayerChoice(PlayerGameInfo player, int minimumBet)
        {
            Console.WriteLine("Your options:");
            if (player.Money > minimumBet)
            {
                if (player.Bet < minimumBet)
                {
                    Console.WriteLine($"C - Call (Match the bet of {minimumBet - player.Bet} chips)");
                }
                else
                {
                    Console.WriteLine("C - Check");
                }
                Console.WriteLine("R - Raise");
            }

            Console.WriteLine("A - All In");
            Console.WriteLine("F - Fold");

            Console.Write("Enter your choice: ");
            return Convert.ToChar(Console.ReadLine().ToUpper());
        }

        public static int GetRaiseAmount()
        {
            Console.Write("How much do you want to raise? ");
            return Convert.ToInt32(Console.ReadLine());
        }

        public static void PrintBlinds(string smallBlindPlayerName, string bigBlindPlayerName, int smallBlindAmount, int bigBlindAmount)
        {
            Console.WriteLine($"{smallBlindPlayerName} posts the Small Blind: {smallBlindAmount} chips.");
            Console.WriteLine($"{bigBlindPlayerName} posts the Big Blind: {bigBlindAmount} chips.");
            Console.WriteLine();
        }

        public static void PrintRoundStart(string roundName)
        {
            Console.WriteLine($"\n--- Starting {roundName} ---\n");
        }
    }
}
