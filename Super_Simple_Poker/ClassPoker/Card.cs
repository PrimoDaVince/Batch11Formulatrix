using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

using Super_Simple_Poker;
namespace Super_Simple_Poker;

public class Card
{
	[JsonPropertyName("idCard")]
	public int IdCard { get; private set; }
	[JsonPropertyName("rank")]
	public RankEnum Rank { get; set; }
	[JsonPropertyName("suit")]
	public SuitEnum Suit { get; set; }

	public Card()
	{ }
	public Card(int idcard, RankEnum rank, SuitEnum suit)
	{
		IdCard = idcard;
		Rank = rank;
		Suit = suit;

	}
}
