using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using PlayingCardJsonTester;
using PlayingCardMakerJSon;

class Program
{
	//Fungsi Buat Find Card Hanya Dgn id nya

	static void Main(string[] args)
	{
		
		Deck deck = new Deck();
	

		string filePath = "./Cards.json";
		
		deck.LoadFromJson(filePath);
		List<Card>? cards = deck.GetAllCards();
		
		Console.WriteLine("\nDeserialized Cards:");
		foreach (var card in  cards)
		{
			Console.WriteLine($"Card: Rank={card.rank}, Suit = {card.suit}, Id = {card.idCard}");
		}
		bool cheker = true;
		while (cheker == true)
		{	
			Console.WriteLine("\nEnter an ID to retrieve the card:");
			if (int.TryParse(Console.ReadLine(), out int cardId))
			{	
				Card foundCard = CardFinder.GetCardById(cards, cardId);
				if (foundCard != null)
				{
					Console.WriteLine($"Found Card: Rank={foundCard.rank}, Suit={foundCard.suit}, Id={foundCard.idCard}");
				}
				else
				{
					Console.WriteLine("Card with the specified ID was not found.");
					cheker = false;
				}
			}
			else
			{
				Console.WriteLine("Invalid ID entered.");

			}
		}
	}

}