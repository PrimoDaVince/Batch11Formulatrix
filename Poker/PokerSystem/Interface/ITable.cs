namespace Poker.PokerSystem.Interface;

public interface ITable
{
	int id { get; }
	string name { get; }
	int minmalBuyIn { get; }
}
