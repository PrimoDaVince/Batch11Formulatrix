namespace PlayingCardJsonTester;
using PlayingCardJsonTester;
using PlayingCardMakerJSon;


public class Hand
{	
	private List<Card> cards;
	

	public Hand()
	{
		
	}

	public void AddCard(Card card)
	{
		if (cards.Count < 2)
		{
			cards.Add(card);
		}
		else
		{
			throw new InvalidOperationException("A hand can only have two cards.");
		}
	}

	public IEnumerable<Card> GetCards()
	{
		return cards;
	}

	public override string ToString()
	{
		return string.Join(", ", cards);
	}
}
