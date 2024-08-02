using System.Text.Json;
using System.Text.Json.Serialization;
using PlayingCardMakerJSon;

class Program
{
	//Fungsi Buat Find Card Hanya Dgn id nya
	private static Card GetCardById(List<Card> cards, int id)
	{
        return cards.FirstOrDefault(card => card.idCard == id);
	}
	static void Main(string[] args)
	{
		string filePath = "./Cards.json";
		using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
		using (StreamReader reader = new StreamReader(fs))
		{
			try
			{
				//deserillized data dari card json
				string jsonFromFile = reader.ReadToEnd();
				List<Card> deserializedCards = JsonSerializer.Deserialize<List<Card>>(jsonFromFile);
				Console.WriteLine("\nDeserialized Cards:");
				foreach (var card in deserializedCards)
				{
					Console.WriteLine($"Card: Rank={card.rank}, Suit = {card.suit}, Id = {card.idCard}");
				}
				//Fungsi Find Card by id
				Console.WriteLine("\nEnter an ID to retrieve the card:");
				if (int.TryParse(Console.ReadLine(), out int cardId))
				{
					Card foundCard = GetCardById(deserializedCards, cardId);
					if (foundCard != null)
					{
						Console.WriteLine($"Found Card: Rank={foundCard.rank}, Suit={foundCard.suit}, Id={foundCard.idCard}");
					}
					else
					{
						Console.WriteLine("Card with the specified ID was not found.");
					}
				}
				else
				{
					Console.WriteLine("Invalid ID entered.");
				}
			}
			catch (FileNotFoundException ex)
			{
				Console.WriteLine($"File not found: {ex.Message}");
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Error deserializing JSON data: {ex.Message}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"An unexpected error occurred: {ex.Message}");
			}
		}
	}

}