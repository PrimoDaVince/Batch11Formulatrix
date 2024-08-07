namespace Poker;

public class PlayerData
{
	private decimal PlayerMoney{ get;  set; }
	private decimal PlayerChips{ get;  set; }
	
	public PlayerData(Player player,decimal playerMoney, decimal playerChips)
	{
	
		PlayerMoney = playerMoney;
		
	}
}
