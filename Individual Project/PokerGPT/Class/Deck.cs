using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Poker
{
	public class Deck
	{
		private readonly Random _random = new Random();
		private List<ICard> _cards;
		public List<Card> listOfCard;

		public Deck(string cardJsonFilePath)
		{
						
			_cards = LoadCardsFromJson(cardJsonFilePath);
			ShuffleDeck();
		}

		public IReadOnlyList<ICard> GetAllCards()
		{
			return _cards.AsReadOnly();
		}

		private void ShuffleDeck()
		{
			_cards = _cards.OrderBy(c => _random.Next()).ToList();
		}

		private List<ICard> LoadCardsFromJson(string filePath)
		{
			try
			{
			 var options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
			Converters = { new JsonStringEnumConverter() }
		};

		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		{
			var cardList = JsonSerializer.Deserialize<List<Card>>(fs, options);

			if (cardList == null || !cardList.Any())
			{
				Console.WriteLine("Deserialization resulted in null or empty list.");
				return new List<ICard>();
			}

			return cardList.Cast<ICard>().ToList();
		}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error loading cards from JSON: {ex.Message}");
				return new List<ICard>();
			}
		}

		public ICard DrawCard()
		{
			if (_cards.Count == 0)
			{
				throw new InvalidOperationException("No cards left in the deck.");
			}
			var card = _cards[0];
			_cards.RemoveAt(0);
			return card;
		}

		public Deck ResetDeck(string cardJsonFilePath)
		{
			_cards = LoadCardsFromJson(cardJsonFilePath);
			ShuffleDeck();
			return this;
		}
	}
}
