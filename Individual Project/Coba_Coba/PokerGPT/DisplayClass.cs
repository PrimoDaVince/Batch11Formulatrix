using System;
using System.Collections.Generic;
using Poker;
using Poker.Enums;

namespace Poker
{
	public class Display
	{
		public void ShowMessage(string message)
		{
			Console.WriteLine(message);
		}

		public void ShowError(string errorMessage)
		{
			Console.WriteLine($"Error: {errorMessage}");
		}

		public string GetPlayerAction(IPlayer player)
		{
			ShowMessage($"{player.Name}, choose your action: Call, Raise, Fold, or Bet");
			return Console.ReadLine()?.ToLower();
		}

		public string GetPlayerName(int playerNumber)
		{
			ShowMessage($"Enter the name for player {playerNumber}: ");
			return Console.ReadLine();
		}

		public decimal GetBetAmount()
		{
			ShowMessage("Enter bet amount: ");
			if (decimal.TryParse(Console.ReadLine(), out decimal amount) && amount > 0)
			{
				return amount;
			}
			ShowMessage("Invalid bet amount.");
			return 0;
		}

		public decimal GetRaiseAmount(decimal currentBet)
		{
			ShowMessage($"Enter raise amount (minimum {currentBet}): ");
			if (decimal.TryParse(Console.ReadLine(), out decimal raiseAmount) && raiseAmount >= currentBet)
			{
				return raiseAmount;
			}
			ShowMessage("Invalid raise amount.");
			return 0;
		}

		public void ShowPlayerHand(PlayerData playerData)
		{
			ShowMessage($"{playerData.Player.Name}'s Hand:");
			foreach (var card in playerData.Hand.GetCards())
			{
				ShowMessage(card.ToString());
			}
		}

		public void ShowCommunityCards(IReadOnlyList<ICard> communityCards)
		{
			ShowMessage("Community Cards:");
			foreach (var card in communityCards)
			{
				ShowMessage(card.ToString());
			}
		}

		public void ShowRoundResults(PlayerData winner, HandRanking handRanking, decimal pot)
		{
			ShowMessage($"Winner: {winner.Player.Name} with {handRanking}");
			ShowMessage($"{winner.Player.Name} wins the pot of {pot} chips!");
		}

		public void ShowCurrentPositions(PlayerData dealer, PlayerData smallBlind, PlayerData bigBlind)
		{
			ShowMessage($"Dealer: {dealer.Player.Name}");
			ShowMessage($"Small Blind: {smallBlind.Player.Name}");
			ShowMessage($"Big Blind: {bigBlind.Player.Name}");
		}

		public string GetContinuationChoice()
		{
			ShowMessage("Press any key for next round or 'q' to quit...");
			return Console.ReadLine()?.ToLower();
		}
	}
}
