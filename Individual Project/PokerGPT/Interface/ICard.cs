using Poker.Enums;

namespace Poker
{
    public interface ICard
    {
        int IdCard { get; }
        Rank Rank { get; }
        Suit Suit { get; }
    }
}
