using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSimplePoker;
namespace SuperSimplePoker;
public struct HandValue
{
	public int Total { get; set; }
	public int HighCard { get; set; }
	public HandRankingEnum Combination { get; set; }
}

public class HandEvaluator
{
	private int heartsSum;
	private int diamondsSum;
	private int clubsSum;
	private int spadesSum;
	private List<Card> cards;
	private HandValue handValue;

	public HandEvaluator(List<Card> sortedHand)
	{
		heartsSum = 0;
		diamondsSum = 0;
		clubsSum = 0;
		spadesSum = 0;
		cards = new List<Card>(sortedHand);
		handValue = new HandValue();
	}

	public HandValue HandValues
	{
		get { return handValue; }
		set { handValue = value; }
	}

	public HandRankingEnum EvaluateHand()
	{
		GetNumberOfSuit();

		if (RoyalFlush())
		{
			handValue.Combination = HandRankingEnum.RoyalFlush;
			return HandRankingEnum.RoyalFlush;
		}
		else if (StraightFlush())
		{
			handValue.Combination = HandRankingEnum.StraightFlush;
			return HandRankingEnum.StraightFlush;
		}
		else if (FourOfKind())
		{
			handValue.Combination = HandRankingEnum.FourOfAKind;
			return HandRankingEnum.FourOfAKind;
		}
		else if (FullHouse())
		{
			handValue.Combination = HandRankingEnum.FullHouse;
			return HandRankingEnum.FullHouse;
		}
		else if (Flush())
		{
			handValue.Combination = HandRankingEnum.Flush;
			return HandRankingEnum.Flush;
		}
		else if (Straight())
		{
			handValue.Combination = HandRankingEnum.Straight;
			return HandRankingEnum.Straight;
		}
		else if (ThreeOfKind())
		{
			handValue.Combination = HandRankingEnum.ThreeOfAKind;
			return HandRankingEnum.ThreeOfAKind;
		}
		else if (TwoPairs())
		{
			handValue.Combination = HandRankingEnum.TwoPair;
			return HandRankingEnum.TwoPair;
		}
		else if (OnePair())
		{
			handValue.Combination = HandRankingEnum.Pair;
			return HandRankingEnum.Pair;
		}

		handValue.HighCard = (int)cards.Last().Rank;
		handValue.Combination = HandRankingEnum.HighCard;
		return HandRankingEnum.HighCard;
	}

	private void GetNumberOfSuit()
	{
		foreach (var card in cards)
		{
			switch (card.Suit)
			{
				case SuitEnum.Hearts:
					heartsSum++;
					break;
				case SuitEnum.Diamonds:
					diamondsSum++;
					break;
				case SuitEnum.Clubs:
					clubsSum++;
					break;
				case SuitEnum.Spades:
					spadesSum++;
					break;
			}
		}
	}

	private bool RoyalFlush()
	{
		return Flush() && cards.Take(5).Select(c => c.Rank).OrderBy(r => r).SequenceEqual(new[] { RankEnum.Ten, RankEnum.Jack, RankEnum.Queen, RankEnum.King, RankEnum.Ace });
	}

	private bool StraightFlush()
	{
		return Straight() && Flush();
	}

	private bool FourOfKind()
	{
		return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 4);
	}

	private bool FullHouse()
	{
		var grouped = cards.GroupBy(c => c.Rank).ToList();
		return grouped.Count == 2 && (grouped[0].Count() == 3 || grouped[1].Count() == 3);
	}

	private bool Flush()
	{
		return heartsSum >= 5 || diamondsSum >= 5 || clubsSum >= 5 || spadesSum >= 5;
	}

	private bool Straight()
	{
		var orderedRanks = cards.Select(c => (int)c.Rank).Distinct().OrderBy(r => r).ToList();
		return orderedRanks.Count >= 5 && orderedRanks.Zip(orderedRanks.Skip(1), (a, b) => b - a).All(d => d == 1);
	}

	private bool ThreeOfKind()
	{
		return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 3);
	}

	private bool TwoPairs()
	{
		return cards.GroupBy(c => c.Rank).Count(g => g.Count() == 2) == 2;
	}

	private bool OnePair()
	{
		return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 2);
	}
}

