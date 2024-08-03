namespace Poker;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Deck
{
	private List<Card>? listOfCards = new List<Card>();
	public List<Card>? GetAllCards()
	{
		return listOfCards;
	}
	public void LoadFromJson(string filePath)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		listOfCards = JsonSerializer.Deserialize<List<Card>>(fs);
	}

}
