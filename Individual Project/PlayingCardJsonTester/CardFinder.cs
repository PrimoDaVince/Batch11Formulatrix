using PlayingCardMakerJSon;

namespace PlayingCardJsonTester;

public class CardFinder : Card
{
	public static Card GetCardById(List<Card> cards, int id)
	{
		return cards.FirstOrDefault(card => card.idCard == id);
	}
}
