namespace Poker;

public class Player:IPlayer
{
	public int Id{ get; private set; }
	private static int _idCounter=1;	
	public string Name{ get; set; }
	
	public Player(int id)
	{
		int Id = id;
	}
	public Player ( string name)
	{	
		Id = _idCounter++;
		Name = name;
	}
}
