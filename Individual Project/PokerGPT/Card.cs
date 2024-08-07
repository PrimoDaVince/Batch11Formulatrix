using Poker.Enums;
namespace Poker;
using System.Text.Json.Serialization;

public class Card : ICard
{
	[JsonPropertyName("idCard")]
	public int IdCard { get; private set; }
	[JsonPropertyName("rank")]
	public Rank Rank { get; private set; }
	[JsonPropertyName("suit")]
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

