using PlayingCardMakerJSon;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace PlayingCardJsonTester;

public class Deck : Card
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
