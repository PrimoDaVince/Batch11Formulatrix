namespace SuperSimplePoker;
using SuperSimplePoker;

public class PlayerGameInfo
{
	public Player Player { get; private set; }
	public List<Card> UnsortedHand { get; set; }
	public int Money { get; set; }
	public int Bet { get; set; }
	public bool PlayerIngame { get; set; }
	public HandEvaluator HandEvaluator { get; set; }

	public PlayerGameInfo(Player player)
	{
		Player = player;
		UnsortedHand = new List<Card>();
		Money = 0;
		Bet = 0;
		PlayerIngame = true;
		HandEvaluator = null;
	}
}

