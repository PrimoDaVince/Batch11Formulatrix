namespace PlayingCardJsonTester;
using PlayingCardMakerJSon;
using System.Text.Json;
using System.Text.Json.Serialization;

public class JsonLoader:Card
{	private List<Card>? listOfCards = new List<Card>();
	public void LoadFromJson()
	{
		using FileStream fs = new FileStream("./Cards.json", FileMode.Open, FileAccess.Read);
		listOfCards =  JsonSerializer.Deserialize<List<Card>>(fs);
	}
	public List<Card>? GetAllJsonCards()
	{
		return listOfCards;
	}
}
