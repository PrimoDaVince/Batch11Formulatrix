using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace Super_Simple_Poker;

	public class DeckOfCards
    {
        private List<Card> deck = new List<Card>(); // list of all playable cards left in deck

        public void LoadFromJson(string filePath)
        {
            using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            deck = JsonSerializer.Deserialize<List<Card>>(fs);
            ShuffleCards();
        }

        public Card DealCard()
        {
            if (deck.Count == 0)
                throw new InvalidOperationException("No more cards in the deck");

            Card card = deck[deck.Count - 1];
            deck.RemoveAt(deck.Count - 1);
            return card;
        }

        // shuffle the deck
        public void ShuffleCards()
        {
            Random rand = new Random();
            int n = deck.Count;
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

	}

