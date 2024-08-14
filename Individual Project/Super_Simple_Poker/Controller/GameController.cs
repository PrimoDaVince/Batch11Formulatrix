using SuperSimplePoker;
using NLog;
using System.Collections.Generic;
using System.Linq;

public class GameController
{
    private readonly DeckOfCards _deck;
    private readonly Table _table;
    private readonly List<PlayerGameInfo> _players;
    private readonly ILogger _log;

    public GameController(DeckOfCards deck, Table table, ILogger log)
    {
        _deck = deck;
        _table = table;
        _log = log;
        _players = new List<PlayerGameInfo>();
    }

    public void InitializePlayer(Player player, int moneyPerPlayer)
    {
        PlayerGameInfo playerGameInfo = new PlayerGameInfo(player)
        {
            Money = moneyPerPlayer,
            PlayerIngame = true
        };
        _players.Add(playerGameInfo);
        _log.Info($"Initialized Player: {player.Name}");
    }

    public List<Card> GetAllCards()
    {
        return _deck.GetAllCards();
    }

    public List<PlayerGameInfo> GetPlayers()
    {
        return _players;
    }

   public void DealHoleCards(List<PlayerGameInfo> players)
{
    foreach (var player in players)
    {
        player.UnsortedHand.Clear();
        player.UnsortedHand.Add(_deck.DealCard());
        player.UnsortedHand.Add(_deck.DealCard());
        player.HandEvaluator = new HandEvaluator(player.UnsortedHand);
        _log.Info($"Dealt hole cards to {player.Player.Name}");
    }
}

    public void DealCommunityCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Card card = _deck.DealCard();
            _table.AddCommunityCard(card);
            _log.Info($"Dealt community card: {card.Rank} of {card.Suit}");
        }
    }

    public List<Card> GetCommunityCards()
    {
        return _table.CommunityCards;
    }

    public void ClearCommunityCards()
    {
        _table.ClearCommunityCards();
    }

    public int GetPot()
    {
        return _table.Pot;
    }

    public void AddToPot(int amount)
    {
        _table.AddToPot(amount);
        _log.Info($"Added {amount} to the pot. Current pot: {_table.Pot}");
    }

    public void ClearPot()
    {
        _table.ClearPot();
        _log.Info("Cleared the pot.");
    }

    public void RemovePlayersWithoutMoney()
    {
        var removedPlayers = _players.Where(player => player.Money <= 0).ToList();
        _players.RemoveAll(player => player.Money <= 0);

        foreach (var player in removedPlayers)
        {
            _log.Info($"Player {player.Player.Name} removed from the game (out of money).");
        }
    }

    public void ClearPlayerBets()
    {
        foreach (var player in _players)
        {
            player.Bet = 0;
        }
    }

    public void ExecuteBlinds(int smallBlindAmount, int bigBlindAmount)
    {
        var smallBlindPlayer = _players[0];
        var bigBlindPlayer = _players[1];

        smallBlindPlayer.Bet = smallBlindAmount;
        bigBlindPlayer.Bet = bigBlindAmount;

        smallBlindPlayer.Money -= smallBlindAmount;
        bigBlindPlayer.Money -= bigBlindAmount;

        AddToPot(smallBlindAmount + bigBlindAmount);

        _log.Info($"{smallBlindPlayer.Player.Name} posts small blind: {smallBlindAmount}");
        _log.Info($"{bigBlindPlayer.Player.Name} posts big blind: {bigBlindAmount}");
    }

    public bool AllPlayersReady(int minimumBet)
    {
        return _players.All(player => player.Money == 0 || player.Bet >= minimumBet);
    }

    public void ExecutePlayerAction(PlayerGameInfo player, char choice, ref int minimumBet)
    {
        if (choice == 'C' && player.Bet < minimumBet)
        {
            int callAmount = minimumBet - player.Bet;
            player.Bet += callAmount;
            player.Money -= callAmount;
            _log.Info($"{player.Player.Name} called with {callAmount}");
        }
        else if (choice == 'C' && player.Bet == minimumBet)
        {
            _log.Info($"{player.Player.Name} checked.");
        }
        else if (choice == 'A')
        {
            int allInMoney = player.Money;
            player.Bet += allInMoney;
            player.Money = 0;
            minimumBet = player.Bet > minimumBet ? player.Bet : minimumBet;
            _log.Info($"{player.Player.Name} went all-in with {allInMoney}");
        }
        else if (choice == 'R')
        {
            int raiseAmount = Display.GetRaiseAmount();
            player.Bet += raiseAmount;
            player.Money -= raiseAmount;
            minimumBet = player.Bet;
            _log.Info($"{player.Player.Name} raised by {raiseAmount}");
        }
        else if (choice == 'F')
        {
            player.PlayerIngame = false;
            _log.Info($"{player.Player.Name} folded.");
        }
    }

    public void AnnounceRoundWinner(PlayerGameInfo winner)
    {
        winner.Money += GetPot();
        ClearPot();
        _log.Info($"{winner.Player.Name} won the round with {winner.HandEvaluator.HandValues.Combination}");
    }

    public PlayerGameInfo DetermineWinner()
    {
        return _players.OrderByDescending(player => player.HandEvaluator.HandValues.Combination).First();
    }
}
