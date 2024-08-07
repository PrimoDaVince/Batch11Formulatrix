using Poker.PokerSystem.Interface;

namespace Poker;

public class Table:ITable
{	
	public int Id{ get; private set; }
	public int MinmalBuyIn{ get; private set; } 
	public int potOnTable{ get; set; }

	private List<Card> communityCards;
	private decimal pot;
	Deck deck = new Deck();
	public Table()
	{	
		communityCards = deck.GetAllCards();
		pot = 0;
	}
	public void AddCardToCommunity(Card card)
	{
		communityCards.Add(card);
	}

	public void AddToPot(decimal amount)
	{
		pot += amount;
	}

	public IEnumerable<Card> CommunityCards => communityCards.AsReadOnly();

	public decimal Pot => pot;

	public void ClearCommunityCards()
	{
		communityCards.Clear();
	}

	public void ClearPot()
	{
		pot = 0;
	}

	
	
	
}

