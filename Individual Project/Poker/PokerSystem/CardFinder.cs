namespace Poker;
// class for finding card by id
public class CardFinder:Card
{
	public static Card GetCardById(List<Card> cards, int id)
	{
		return cards.FirstOrDefault(card => card.idCard == id);
	}
}
