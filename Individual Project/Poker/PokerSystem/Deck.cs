namespace Poker;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Deck
{
	Random random= new Random();
	private List<Card>? listOfCards = new List<Card>();
	private List<Card>? DeckCard = new();
	public Deck()
	{
		GetSuffledDeck();
	
	}
	
	public List<Card>? GetAllCards()
	{
		return DeckCard;
	}
	private List<Card>? GetSuffledDeck()
	{
		return DeckCard = listOfCards.OrderBy(c=>random.Next()).ToList();
	}
	public void LoadCardJson(string filePath)
	{
		using FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
		listOfCards = JsonSerializer.Deserialize<List<Card>>(fs);
	}
}
