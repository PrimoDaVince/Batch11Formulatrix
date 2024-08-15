namespace SuperSimplePoker
{
    public static class Display
    {
        public static void PrintPlayerStatus(string playerName, int money, int currentBet, int minimumBet)
        {
            Console.WriteLine($"--- {playerName}'s Turn ---");
            Console.WriteLine($"Money left: {money}");
            Console.WriteLine($"Current Bet: {currentBet}");
            Console.WriteLine($"Minimum Bet to Call: {minimumBet}");
            Console.WriteLine();
        }

        public static void PrintPlayerJoinQuitOptions()
        {
            Console.WriteLine("\nWould anyone like to join or quit the game?");
            Console.WriteLine("Enter 'J' to join, 'Q' to quit, or press Enter to continue:");
        }

        public static void PrintPlayerOptions(string options)
        {
            Console.WriteLine(options);
        }

        public static void PromptPlayerChoice()
        {
            Console.Write("Enter your choice (K for Check, C for Call, R for Raise, A for All-In, F for Fold): ");
        }

        public static void PromptRaiseAmount()
        {
            Console.Write("How much do you want to raise? ");
        }

        public static void PrintRoundStart(string roundName)
        {
            Console.WriteLine($"\n--- {roundName} Betting Round ---\n");
        }

        public static void PrintRoundWinner(string playerName, string winningHand)
        {
            Console.WriteLine($"\n{playerName} won the round with {winningHand}!");
        }

        public static void PrintUltimateWinner(string playerName, int totalMoney)
        {
            Console.WriteLine($"\n{playerName} is the ultimate winner with a total of {totalMoney} chips!");
        }

        public static void PrintPlayerCard(string cardDescription, int cardNumber)
        {
            Console.WriteLine($"Hole Card {cardNumber}: {cardDescription}");
        }

        public static void PrintCommunityCard(string cardDescription, int cardNumber)
        {
            Console.WriteLine($"Community Card {cardNumber}: {cardDescription}");
        }

        public static void PrintCallAmount(int callAmount)
        {
            Console.WriteLine($"You called for {callAmount} chips.");
        }

        public static void PrintRaiseAmount(int raiseAmount)
        {
            Console.WriteLine($"You raised by {raiseAmount} chips.");
        }

        public static void PrintFold()
        {
            Console.WriteLine("You folded.");
        }

        public static void PrintAllIn(int allInAmount)
        {
            Console.WriteLine($"You went all-in with {allInAmount} chips.");
        }

        public static void PrintCheck()
        {
            Console.WriteLine("You checked.");
        }

        public static void PrintNewLine()
        {
            Console.WriteLine();
        }
    }
}
