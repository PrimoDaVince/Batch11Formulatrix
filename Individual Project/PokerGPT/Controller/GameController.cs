
using System;
using System.Collections.Generic;
using System.Linq;
using Poker;
using Poker.Enums;


namespace Poker;

public class GameController
{
	private readonly List<PlayerData> _players;
	private readonly ITable _table;
	private readonly Deck _deck;
	private readonly HandRankComparator _comparator;
	private readonly Position _dealerPosition;
	private readonly Position _smallBlindPosition;
	private readonly Position _bigBlindPosition;
	private readonly Display _display;
	private const decimal SmallBlindAmount = 10m;
	private const decimal BigBlindAmount = 20m;
	private decimal _currentBet;
	private bool _roundOver;
	private readonly string _cardJsonFilePath;

	public GameController(List<PlayerData> players, ITable table, Deck deck, string cardJsonFilePath)
	{
		_players = players;
		_table = table;
		_deck = deck;
		_comparator = new HandRankComparator();
		_display = new Display();
		_cardJsonFilePath = cardJsonFilePath;

		// Initialize positions
		_dealerPosition = new Position(0);
		_smallBlindPosition = new Position(1 % _players.Count);
		_bigBlindPosition = new Position(2 % _players.Count);

		_currentBet = 0;
		_roundOver = false;
	}

	public void StartGame()
	{
		while (true) // Main game loop
		{
			try
			{
				DealBlinds();
				DealCards();
				_roundOver = false;
				_currentBet = BigBlindAmount;

				while (!_roundOver)
				{
					ExecuteBettingRound();
				}

				DealCommunityCards();
				DetermineWinner();
				RotatePositions();

				// Break condition for demo purposes
				if (_display.GetContinuationChoice() == "q")
				{
					break;
				}

				_deck.ResetDeck(_cardJsonFilePath); // Reset and reshuffle deck
				_table.ClearCommunityCards();
				_table.ClearPot();
			}
			catch (Exception ex)
			{
				_display.ShowError($"An error occurred during the game: {ex.Message}");
			}
		}
	}

	private void DealBlinds()
	{
		var smallBlindPlayer = _players[_smallBlindPosition.Index];
		var bigBlindPlayer = _players[_bigBlindPosition.Index];

		smallBlindPlayer = PlaceBet(smallBlindPlayer, SmallBlindAmount);
		bigBlindPlayer = PlaceBet(bigBlindPlayer, BigBlindAmount);

		_display.ShowMessage($"{smallBlindPlayer.Player.Name} posts small blind of {SmallBlindAmount}");
		_display.ShowMessage($"{bigBlindPlayer.Player.Name} posts big blind of {BigBlindAmount}");
	}

	private PlayerData PlaceBet(PlayerData playerData, decimal amount)
	{
		if (playerData.Chips >= amount)
		{
			playerData.UpdateBet(amount);
			_table.AddToPot(amount);
		}
		else
		{
			_display.ShowError($"{playerData.Player.Name} doesn't have enough chips for the bet.");
			playerData.Fold();
		}
		return playerData;
	}

	public void DealCards()
	{
		foreach (var playerData in _players)
		{
			playerData.Hand.Clear();
			try
			{
				for (int i = 0; i < 2; i++)
				{
					playerData.Hand.AddCard(_deck.DrawCard());
				}
			}
			catch (InvalidOperationException ex)
			{
				_display.ShowError($"Error dealing cards to {playerData.Player.Name}: {ex.Message}");
			}
		}
	}

	private void ExecuteBettingRound()
	{
		for (int i = 0; i < _players.Count; i++)
		{
			var playerData = _players[i];
			if (playerData.Status == PlayerStatus.Folded) continue;

			string action = _display.GetPlayerAction(playerData.Player);
			switch (action)
			{
				case "call":
					playerData = Call(playerData);
					break;
				case "raise":
					playerData = Raise(playerData);
					break;
				case "fold":
					playerData = Fold(playerData);
					break;
				case "bet":
					playerData = Bet(playerData);
					break;
				default:
					_display.ShowMessage("Invalid action, try again.");
					break;
			}

			if (_players.Count(p => p.Status == PlayerStatus.Active) == 1)
			{
				_roundOver = true;
				break;
			}
		}
	}

	private PlayerData Call(PlayerData playerData)
	{
		decimal amountToCall = _currentBet - playerData.CurrentBet;

		if (playerData.Chips >= amountToCall)
		{
			playerData.UpdateBet(_currentBet);
			_table.AddToPot(amountToCall);
			_display.ShowMessage($"{playerData.Player.Name} calls with {amountToCall}.");
		}
		else
		{
			_display.ShowError($"{playerData.Player.Name} doesn't have enough chips to call.");
		}
		return playerData;
	}

	private PlayerData Raise(PlayerData playerData)
	{
		decimal raiseAmount = _display.GetRaiseAmount(_currentBet);
		if (raiseAmount >= _currentBet)
		{
			decimal totalAmount = _currentBet + raiseAmount;

			if (playerData.Chips >= totalAmount)
			{
				playerData.UpdateBet(totalAmount);
				_currentBet = totalAmount;
				_table.AddToPot(totalAmount);
				_display.ShowMessage($"{playerData.Player.Name} raises to {totalAmount}.");
			}
			else
			{
				_display.ShowError($"{playerData.Player.Name} doesn't have enough chips to raise.");
			}
		}
		return playerData;
	}

	private PlayerData Fold(PlayerData playerData)
	{
		playerData.Fold();
		_display.ShowMessage($"{playerData.Player.Name} folds.");
		return playerData;
	}

	private PlayerData Bet(PlayerData playerData)
	{
		decimal betAmount = _display.GetBetAmount();
		if (betAmount > 0)
		{
			if (playerData.Chips >= betAmount)
			{
				playerData.UpdateBet(betAmount);
				_currentBet = Math.Max(_currentBet, betAmount);
				_table.AddToPot(betAmount);
				_display.ShowMessage($"{playerData.Player.Name} bets {betAmount}.");
			}
			else
			{
				_display.ShowError($"{playerData.Player.Name} doesn't have enough chips to bet.");
			}
		}
		return playerData;
	}

	public void DealCommunityCards()
	{
		try
		{
			for (int i = 0; i < 5; i++)
			{
				_table.AddCardToCommunity(_deck.DrawCard());
			}
			_display.ShowCommunityCards(_table.CommunityCards);
		}
		catch (InvalidOperationException ex)
		{
			_display.ShowError($"Error dealing community cards: {ex.Message}");
		}
		
	}

	public void DetermineWinner()
	{
		PlayerData winner = null;
		HandRanking bestHandRank = HandRanking.HighCard;
		var winningHand = new List<ICard>();

		foreach (var playerData in _players)
		{
			if (playerData.Status == PlayerStatus.Folded) continue;

			var bestHand = playerData.Hand.BestHand(_table.CommunityCards);
			var currentHandRank = _comparator.DetermineBestHand(bestHand, _table.CommunityCards);

			if (currentHandRank > bestHandRank)
			{
				bestHandRank = currentHandRank;
				winner = playerData;
				winningHand = bestHand.ToList();
			}
		}

		if (winner != null)
		{
			_display.ShowRoundResults(winner, bestHandRank, _table.Pot);
			_display.ShowPlayerHand(winner);
			winner.Chips += _table.Pot;
		}
		else
		{
			_display.ShowMessage("No winner determined.");
		}
	}

	private void RotatePositions()
	{
		_dealerPosition.Rotate(_players.Count);
		_smallBlindPosition.Rotate(_players.Count);
		_bigBlindPosition.Rotate(_players.Count);

		_display.ShowCurrentPositions(
			_players[_dealerPosition.Index],
			_players[_smallBlindPosition.Index],
			_players[_bigBlindPosition.Index]);

		// Reset player statuses for the next round
		_players.ForEach(p => p.ResetStatus());
	}
}

