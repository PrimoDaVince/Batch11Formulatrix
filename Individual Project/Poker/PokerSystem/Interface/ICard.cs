namespace Poker.PokerSystem.Interface;
using SuitEnum;
using RankEnum;

public interface ICard
{
	 public int idCard { get;  }
	 public Rank rank { get;  }
	 public Suit suit { get;  }
}
