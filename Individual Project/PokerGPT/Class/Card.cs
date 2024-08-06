using Poker.Enums;
namespace Poker;
using System.Text.Json.Serialization;
public class Card : ICard
{
    public int IdCard { get;  set; }
    public Rank Rank { get;  set; }
    public Suit Suit { get;  set; }

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

