namespace Poker.PokerSystem.Interface;
using SuitEnum;
using RankEnum;

public interface ICard
{
	 public int IdCard { get;  }
	 public Rank Rank { get;  }
	 public Suit Suit { get;  }
}
