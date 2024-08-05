namespace Poker;

public class PlayerData
{
	private decimal PlayerMoney{ get;  set; }
	private decimal PlayerChips{ get;  set; }
	
	public PlayerData(decimal playerMoney, decimal playerChips)
	{
		PlayerChips = playerChips;
		PlayerMoney = playerMoney;
	}
}
