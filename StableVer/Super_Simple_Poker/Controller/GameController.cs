namespace Super_Simple_Poker;
using Super_Simple_Poker;

public class GameController
    {
        public List<PlayerGameInfo> Players { get; private set; }
        public List<Card> CommunityCards { get; private set; }
        public int MinimumBet { get; private set; }
        private DeckOfCards deck;

        public GameController(int playerCount, int moneyPerPlayer, DeckOfCards deck)
        {
            this.deck = deck;
            Players = new List<PlayerGameInfo>();
            CommunityCards = new List<Card>();
            InitializePlayers(playerCount, moneyPerPlayer);
        }

        private void InitializePlayers(int playerCount, int moneyPerPlayer)
        {
            for (int i = 0; i < playerCount; i++)
            {
                Player player = new Player(i + 1, "Player " + (i + 1));
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

                CommunityCards = new List<Card>();
                foreach (var player in Players)
                {
                    player.Bet = 0;
                }

                int pot = 0;

                // -- GAMEPLAY --

                // ---- FLOP ----
                pot += Round();

                Display.PrintPot(pot);

                DealCommunityCards(3); // Flop
                Display.PrintCommunityCards(CommunityCards);

                pot += Round();
                Display.PrintPot(pot);

                DealCommunityCards(1); // Turn
                Display.PrintCommunityCards(CommunityCards);

                pot += Round();
                Display.PrintPot(pot);

                DealCommunityCards(1); // River
                Display.PrintCommunityCards(CommunityCards);

                pot += Round();

                AssignBestCombinations();
                AnnounceWinner(pot);

                RemovePlayersWithoutMoney();
            }
        }

        private int Round()
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
                        Display.PrintPlayerStatus(Players[i], MinimumBet, CommunityCards);
                        char choice = Display.GetPlayerChoice(Players[i], MinimumBet);

                        ExecutePlayerChoice(Players[i], choice);
                        Display.PrintNewLine();
                    }
                }
            }

            int collectedAmount = Players.Sum(player => player.Bet);
            Players.ForEach(player => player.Bet = 0);

            return collectedAmount;
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
                CommunityCards.Add(deck.DealCard());
            }
        }

        private void AssignBestCombinations()
        {
            foreach (var player in Players)
            {
                List<Card> combinedHand = CardSort(player.UnsortedHand, CommunityCards);
                Display.PrintPlayerCards(player.Player.Name, combinedHand);
                player.HandEvaluator = new HandEvaluator(combinedHand);
                Display.PrintBestCombination(player.Player.Name, player.HandEvaluator.EvaluateHand());
            }
        }

        private void AnnounceWinner(int pot)
        {
            Players = Players.OrderBy(player => player.HandEvaluator.HandValues.Combination).ToList();
            PlayerGameInfo winner = Players.Last();
            Display.PrintRoundWinner(winner.Player.Name, winner.HandEvaluator.HandValues.Combination);
            winner.Money += pot;
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
    






