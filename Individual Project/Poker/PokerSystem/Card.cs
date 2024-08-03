using Poker.PokerSystem.Interface;
using RankEnum;
using SuitEnum;

namespace Poker;

public class Card:ICard
{
	public int idCard { get; private set; }
	public Rank rank { get; private set; }
	public Suit suit { get; private set; }
	
	public Card()
	{}
	public Card(int Idcard,Rank rankD,Suit suitD)
	{
		idCard = Idcard;
		rank= rankD;
		suit=suitD;
		
	}
}
