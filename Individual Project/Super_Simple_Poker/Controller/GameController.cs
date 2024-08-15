using SuperSimplePoker;
using NLog;
using System.Collections.Generic;
using System.Linq;

public class GameController
{
	private readonly DeckOfCards _deck;
	private readonly Table _table;
	private readonly ILogger _log;
	private readonly List<PlayerGameInfo> _players;
	private int _dealerPosition;

	public GameController(DeckOfCards deck, Table table, ILogger log)
	{
		_deck = deck;
		_table = table;
		_log = log;
		_players = new List<PlayerGameInfo>();
		_dealerPosition = 0;  // Start with the first player as the dealer
	}
	 public List<string> GetCommunityCards()
    {
        return _table.CommunityCards.Select(card => $"{card.Rank} of {card.Suit}").ToList();
    }

	// Method to initialize a player
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

	// Method to deal hole cards
	public void DealHoleCards()
	{
		foreach (var player in _players)
		{
			if (player.PlayerIngame)
			{
				player.ClearHand();
				player.AddCard(_deck.DealCard());
				player.AddCard(_deck.DealCard());
				_log.Info($"Dealt hole cards to {player.Player.Name}");
			}
		}
	}

	// Method to deal community cards
	public void DealCommunityCards(int count)
	{
		for (int i = 0; i < count; i++)
		{
			Card card = _deck.DealCard();
			_table.AddCommunityCard(card);
			_log.Info($"Dealt community card: {card.Rank} of {card.Suit}");
		}
	}

	// Method to post blinds, using the dealer position to determine who posts the blinds
	public void PostBlinds(int smallBlind, int bigBlind)
	{
		var smallBlindPosition = GetNextPlayerPosition(_dealerPosition);
		var bigBlindPosition = GetNextPlayerPosition(smallBlindPosition);

		var smallBlindPlayer = _players[smallBlindPosition];
		var bigBlindPlayer = _players[bigBlindPosition];

		smallBlindPlayer.Bet = smallBlind;
		bigBlindPlayer.Bet = bigBlind;

		smallBlindPlayer.Money -= smallBlind;
		bigBlindPlayer.Money -= bigBlind;

		_table.AddToPot(smallBlind + bigBlind);

		_log.Info($"{smallBlindPlayer.Player.Name} posted the small blind: {smallBlind}");
		_log.Info($"{bigBlindPlayer.Player.Name} posted the big blind: {bigBlind}");
	}

	// Method to rotate the dealer
	public void RotateDealer()
	{
		_dealerPosition = GetNextPlayerPosition(_dealerPosition);
	}

	// Get the position of the next active player
	private int GetNextPlayerPosition(int currentPosition)
	{
		int nextPosition = (currentPosition + 1) % _players.Count;
		while (!_players[nextPosition].PlayerIngame)
		{
			nextPosition = (nextPosition + 1) % _players.Count;
		}
		return nextPosition;
	}

	// Method to get player statuses
	public IEnumerable<(string Name, int Money, int Bet, bool PlayerIngame, List<string> Cards)> GetPlayerStatuses()
	{
		return _players.Select(player => (player.Player.Name, player.Money, player.Bet, player.PlayerIngame, player.UnsortedHand.Select(c => $"{c.Rank} of {c.Suit}").ToList()));
	}

	// Method to handle player quitting the game
	public void PlayerQuit(string playerName)
	{
		var player = _players.FirstOrDefault(p => p.Player.Name == playerName);
		if (player != null)
		{
			_players.Remove(player);
			_log.Info($"{player.Player.Name} has quit the game.");
		}
	}

	// Method to handle player joining the game
	public void PlayerJoin(Player newPlayer, int money)
	{
		InitializePlayer(newPlayer, money);
		_log.Info($"{newPlayer.Name} has joined the game with {money} chips.");
	}

	// Method to get the number of players
	public int GetPlayerCount()
	{
		return _players.Count;
	}

	// Method to determine the winner of the showdown
	public PlayerGameInfo DetermineWinner()
	{
		return _players.OrderByDescending(player => player.HandEvaluator.HandValues.Combination).FirstOrDefault();
	}

	// Method to process when a player calls
	public void PlayerCall(string playerName, int callAmount)
	{
		var player = _players.FirstOrDefault(p => p.Player.Name == playerName);
		if (player != null && player.PlayerIngame)
		{
			player.Money -= callAmount;
			player.Bet += callAmount;
			_table.AddToPot(callAmount);
			_log.Info($"{player.Player.Name} called with {callAmount} chips.");
		}
	}

	// Method to process when a player raises
	public void PlayerRaise(string playerName, int raiseAmount)
	{
		var player = _players.FirstOrDefault(p => p.Player.Name == playerName);
		if (player != null && player.PlayerIngame)
		{
			player.Money -= raiseAmount;
			player.Bet += raiseAmount;
			_table.AddToPot(raiseAmount);
			_log.Info($"{player.Player.Name} raised with {raiseAmount} chips.");
		}
	}

	// Method to process when a player folds
	public void PlayerFold(string playerName)
	{
		var player = _players.FirstOrDefault(p => p.Player.Name == playerName);
		if (player != null)
		{
			player.PlayerIngame = false;
			_log.Info($"{player.Player.Name} folded.");
		}
	}

	// Method to process when a player goes all-in
	public void PlayerAllIn(string playerName)
	{
		var player = _players.FirstOrDefault(p => p.Player.Name == playerName);
		if (player != null && player.PlayerIngame)
		{
			_table.AddToPot(player.Money);
			player.Bet += player.Money;
			_log.Info($"{player.Player.Name} went all-in with {player.Money} chips.");
			player.Money = 0;
		}
	}

	// Method to clear the pot after a round
	public void ClearPot()
	{
		_table.ClearPot();
		_log.Info("Cleared the pot.");
	}

	// Method to remove players with no money left
	public void RemovePlayersWithoutMoney()
	{
		var removedPlayers = _players.Where(player => player.Money <= 0).ToList();
		_players.RemoveAll(player => player.Money <= 0);

		foreach (var player in removedPlayers)
		{
			_log.Info($"Player {player.Player.Name} has been removed from the game (out of money).");
		}
	}

	// Method to check if only one player is left in the game
	public bool IsOnlyOnePlayerRemaining()
	{
		return _players.Count(p => p.PlayerIngame) == 1;
	}
}



