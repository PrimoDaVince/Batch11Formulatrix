namespace Poker;
using System.Text.Json;
using System.Text.Json.Serialization;
public class Deck
{
	Random random= new Random();
	private List<Card>? listOfCards = new List<Card>();
	private List<Card>? DeckCard = new();
	public Deck()
	{   string filepath =@"C:\Users\ACER\Desktop\Bootcamp11Formulatrix\clone - Copy\Batch11Formulatrix\Individual Project\Poker\PokerSystem\JSON\Cards.json";	
		LoadCardJson(filepath);
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
	public void LoadCardJson(string filepath)
	{
		using FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
		listOfCards = JsonSerializer.Deserialize<List<Card>>(fs);
	}
}
