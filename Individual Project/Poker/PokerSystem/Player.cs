namespace Poker;

public class Player:IPlayer
{
	public int id{ get; set; }
	public string? name{ get; set; }
	
	public Player(int Id)
	{
		int id = Id;
	}
	public Player (int Id, string Name)
	
	{
		id = Id;
		name = Name;
	}
}
