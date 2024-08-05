using System.Runtime.CompilerServices;

namespace Poker;

public class GameController
{
	static private 
	private List<Player> players;
	private Table table;
	private Deck deck;
	private PlayerData playerData;
	public GameController(List<Player> players)
	{
		deck = new Deck();
		this.players = players;
		table = new Table();
		

	}
}
