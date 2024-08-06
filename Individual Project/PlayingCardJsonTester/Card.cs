using System.Data.Common;
using System.Text.Json.Serialization;

namespace PlayingCardMakerJSon;

public class Card 
{
		
	[JsonPropertyName("idCard")]
	public int idCard { get; set; }
	
	[JsonPropertyName("rank")]
	public Rank rank { get; set; }
	
	[JsonPropertyName("suit")]
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
