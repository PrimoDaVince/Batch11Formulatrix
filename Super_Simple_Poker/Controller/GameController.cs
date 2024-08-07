namespace Super_Simple_Poker;
using Super_Simple_Poker;


   public class GameController
    {
        public List<PlayerGameInfo> Players { get; private set; }
        public Table Table { get; private set; }
        public int MinimumBet { get; private set; }
        private DeckOfCards deck;

        public GameController(List<string> playerNames, int moneyPerPlayer, DeckOfCards deck)
        {
            this.deck = deck;
            Players = new List<PlayerGameInfo>();
            Table = new Table();
            InitializePlayers(playerNames, moneyPerPlayer);
        }

        private void InitializePlayers(List<string> playerNames, int moneyPerPlayer)
        {
            for (int i = 0; i < playerNames.Count; i++)
            {
                Player player = new Player(i + 1, playerNames[i]);
                PlayerGameInfo playerGameInfo = new PlayerGameInfo(player)
                {
                    Money = moneyPerPlayer,
                    PlayerIngame = true
                };
                playerGameInfo.UnsortedHand.Add(deck.DealCard());
                playerGameInfo.UnsortedHand.Add(deck.DealCard());

                playerGameInfo.HandEvaluator = new HandEvaluator(playerGameInfo.UnsortedHand);
                Players.Add(playerGameInfo);
            }
        }

        public void StartGame()
        {
            while (Players.Count > 1)
            {
                MinimumBet = Players[0].Money / 50;

                Table.ClearCommunityCards();
                foreach (var player in Players)
                {
                    player.Bet = 0;
                }

                Table.ClearPot();

                // -- GAMEPLAY --

                // ---- FLOP ----
                Round();

                Display.PrintPot(Table.Pot);

                DealCommunityCards(3); // Flop
                Display.PrintCommunityCards(Table.CommunityCards);

                Round();
                Display.PrintPot(Table.Pot);

                DealCommunityCards(1); // Turn
                Display.PrintCommunityCards(Table.CommunityCards);

                Round();
                Display.PrintPot(Table.Pot);

                DealCommunityCards(1); // River
                Display.PrintCommunityCards(Table.CommunityCards);

                Round();

                AssignBestCombinations();
                AnnounceWinner();

                RemovePlayersWithoutMoney();
            }
        }

        private void Round()
        {
            bool roundComplete = false;

            while (!roundComplete)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (AllPlayersReady())
                    {
                        roundComplete = true;
                        break;
                    }

                    if (Players[i].PlayerIngame)
                    {
                        Display.PrintPlayerStatus(Players[i], MinimumBet, Table.CommunityCards);
                        char choice = Display.GetPlayerChoice(Players[i], MinimumBet);

                        ExecutePlayerChoice(Players[i], choice);
                        Display.PrintNewLine();
                    }
                }
            }

            int collectedAmount = Players.Sum(player => player.Bet);
            Table.AddToPot(collectedAmount);
            Players.ForEach(player => player.Bet = 0);
        }

        private bool AllPlayersReady()
        {
            return Players.All(player => player.Money == 0 || player.Bet >= MinimumBet);
        }

        private void ExecutePlayerChoice(PlayerGameInfo player, char choice)
        {
            if (choice == 'C' && player.Bet < MinimumBet)
            {
                int callAmount = MinimumBet - player.Bet;
                Display.PrintCallAmount(callAmount);
                player.Bet += callAmount;
                player.Money -= callAmount;
            }
            else if (choice == 'C' && player.Bet == MinimumBet)
            {
                Display.PrintCheck();
            }
            else if (choice == 'A')
            {
                int allInMoney = player.Money;
                player.Money = 0;
                player.Bet += allInMoney;
                Display.PrintAllIn(allInMoney);
                if (allInMoney > MinimumBet) MinimumBet += allInMoney;
            }
            else if (choice == 'R')
            {
                int raiseAmount = Display.GetRaiseAmount();
                if (raiseAmount < player.Money)
                {
                    player.Bet += raiseAmount;
                    player.Money -= raiseAmount;
                    player.Bet += MinimumBet;
                    player.Money -= MinimumBet;
                    MinimumBet += raiseAmount;
                    Display.PrintRaiseAmount(raiseAmount);
                }
            }
            else if (choice == 'F')
            {
                Display.PrintFold();
                player.PlayerIngame = false;
            }
        }

        private void DealCommunityCards(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Table.AddCommunityCard(deck.DealCard());
            }
        }

        private void AssignBestCombinations()
        {
            foreach (var player in Players)
            {
                List<Card> combinedHand = CardSort(player.UnsortedHand, Table.CommunityCards);
                Display.PrintPlayerCards(player.Player.Name, combinedHand);
                player.HandEvaluator = new HandEvaluator(combinedHand);
                Display.PrintBestCombination(player.Player.Name, player.HandEvaluator.EvaluateHand());
            }
        }
        private void AnnounceWinner()
        {
            Players = Players.OrderBy(player => player.HandEvaluator.HandValues.Combination).ToList();
            PlayerGameInfo winner = Players.Last();
            Display.PrintRoundWinner(winner.Player.Name, winner.HandEvaluator.HandValues.Combination);
            winner.Money += Table.Pot;
            Display.PrintNewLine();
        }

        private void RemovePlayersWithoutMoney()
        {
            Players.RemoveAll(player => player.Money <= 0);
            foreach (var player in Players.Where(player => player.Money <= 0))
            {
                Display.PrintPlayerKickedOut(player.Player.Name);
            }

            if (Players.Count == 1)
            {
                Display.PrintUltimateWinner(Players[0].Player.Name, Players[0].Money);
            }
        }

        public List<Card> CardSort(List<Card> playerCards, List<Card> communityCards)
        {
            List<Card> totalCards = new List<Card>(playerCards);
            totalCards.AddRange(communityCards);

            return totalCards.OrderBy(card => card.Rank).ToList();
        }
    }
    
    
    






