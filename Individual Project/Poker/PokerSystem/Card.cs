using RankEnum;
using SuitEnum;

namespace Poker;

public class Card
{
	public int idCard { get; private set; }
	public Rank rank { get; set; }
	public Suit suit { get; set; }
	
	public Card()
	{}
	public Card(int Idcard,Rank rankd,Suit suitd)
	{
		idCard = Idcard;
		rank= rankd;
		suit=suitd;
		
	}
}
