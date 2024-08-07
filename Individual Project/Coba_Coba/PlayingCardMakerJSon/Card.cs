using System.Data.Common;

namespace PlayingCardMakerJSon;

public class Card
{
	private static int _idCounter=1;	
	public int idCard { get; private set; }
	public Rank rank { get; set; }
	public Suit suit { get; set; }
	
	public Card(Rank Rrank,Suit Ssuit)
	{
		idCard = _idCounter++;
		rank= Rrank;
		suit=Ssuit;
		
	}
}
