using Poker.PokerSystem.Interface;

namespace Poker;

public class Table:ITable
{
	public int id{ get; set; }
	public int maxPlayer{ get; set; }
	public int minPlayer{ get; set; }
	 
	public int minmalBuyIn{ get; set; } 
	public int potOnTable{ get; set; }
	
}
