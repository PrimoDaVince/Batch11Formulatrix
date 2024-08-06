using Poker.Enums;
namespace Poker;

public class Card : ICard
{
    public int IdCard { get; private set; }
    public Rank Rank { get; private set; }
    public Suit Suit { get; private set; }

    public Card(int idCard, Rank rank, Suit suit)
    {
        IdCard = idCard;
        Rank = rank;
        Suit = suit;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }
}

