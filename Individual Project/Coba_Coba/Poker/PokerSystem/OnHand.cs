namespace Poker;

public class OnHand
{	private List<Card>? cards;
	private const int MaxCards = 2;
	Deck deck =new();
	public OnHand()
	{
		cards = deck.GetAllCards();
	}

	public void AddCard(Card card)
	{
		if (cards.Count >= MaxCards)
		{
			throw new InvalidOperationException("A hand can only contain two cards.");
		}
		cards.Add(card);
	}

	public void Clear()
	{
		cards.Clear();
	}

	public List<Card> GetCards()
	{
		return new List<Card>(cards);
	}
}
