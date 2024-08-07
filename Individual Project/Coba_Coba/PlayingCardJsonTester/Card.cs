using System.Data.Common;

namespace PlayingCardMakerJSon;

public class Card 
{
		
	  
	public int idCard { get; set; }
	public Rank rank { get; set; }
	public Suit suit { get; set; }
	
	public Card()
	{}
	public Card(int Idcard,Rank Rrank,Suit Ssuit)
	{
		idCard = Idcard;
		rank= Rrank;
		suit=Ssuit;
		
	}
}
