using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Enums;

namespace Poker;


   public class HandRankComparator
    {
        public HandRanking EvaluateHandRank(IEnumerable<ICard> hand)
        {
            var (rank, _) = EvaluateHand(hand);
            return rank;
        }

        public (HandRanking, IEnumerable<ICard>) DetermineBestHand(IEnumerable<ICard> allCards)
        {
            var bestHand = new List<ICard>();
            var bestRank = HandRanking.HighCard;

            var allCombinations = GetCombinations(allCards, 5).ToList();

            foreach (var combination in allCombinations)
            {
                var (handRank, handCards) = EvaluateHand(combination);

                if (handRank > bestRank)
                {
                    bestRank = handRank;
                    bestHand = handCards.ToList();
                }
            }

            return (bestRank, bestHand);
        }

        public int CompareHands(IEnumerable<ICard> hand1, IEnumerable<ICard> hand2)
        {
            // Evaluate both hands
            var (rank1, cards1) = EvaluateHand(hand1);
            var (rank2, cards2) = EvaluateHand(hand2);

            // Compare hand ranks
            int rankComparison = rank1.CompareTo(rank2);
            if (rankComparison != 0)
                return rankComparison;

            // If ranks are the same, compare the high cards
            var sortedCards1 = cards1.OrderByDescending(c => c.Rank).ToList();
            var sortedCards2 = cards2.OrderByDescending(c => c.Rank).ToList();

            for (int i = 0; i < sortedCards1.Count; i++)
            {
                int cardComparison = sortedCards1[i].Rank.CompareTo(sortedCards2[i].Rank);
                if (cardComparison != 0)
                    return cardComparison;
            }

            return 0; // Hands are completely equal
        }

        public (HandRanking, IEnumerable<ICard>) EvaluateHand(IEnumerable<ICard> cards)
        {
            var allCards = cards.ToList();

            if (allCards.Count < 5)
            {
                throw new InvalidOperationException("Not enough cards to evaluate a hand.");
            }

            if (IsRoyalFlush(allCards, out var royalFlushCards))
                return (HandRanking.RoyalFlush, royalFlushCards);
            if (IsStraightFlush(allCards, out var straightFlushCards))
                return (HandRanking.StraightFlush, straightFlushCards);
            if (IsFourOfAKind(allCards, out var fourOfAKindCards))
                return (HandRanking.FourOfAKind, fourOfAKindCards);
            if (IsFullHouse(allCards, out var fullHouseCards))
                return (HandRanking.FullHouse, fullHouseCards);
            if (IsFlush(allCards, out var flushCards))
                return (HandRanking.Flush, flushCards);
            if (IsStraight(allCards, out var straightCards))
                return (HandRanking.Straight, straightCards);
            if (IsThreeOfAKind(allCards, out var threeOfAKindCards))
                return (HandRanking.ThreeOfAKind, threeOfAKindCards);
            if (IsTwoPair(allCards, out var twoPairCards))
                return (HandRanking.TwoPair, twoPairCards);
            if (IsOnePair(allCards, out var onePairCards))
                return (HandRanking.OnePair, onePairCards);

            return (HandRanking.HighCard, allCards.OrderByDescending(c => c.Rank).Take(5));
        }

        private IEnumerable<IEnumerable<T>> GetCombinations<T>(IEnumerable<T> elements, int combinationLength)
        {
            var list = elements.ToList();
            return GetCombinations(list, combinationLength, 0);
        }

        private IEnumerable<IEnumerable<T>> GetCombinations<T>(List<T> list, int combinationLength, int start)
        {
            if (combinationLength == 0)
                return new[] { Enumerable.Empty<T>() };

            return
                from i in Enumerable.Range(start, list.Count)
                from combination in GetCombinations(list, combinationLength - 1, i + 1)
                select new[] { list[i] }.Concat(combination);
        }


    private bool IsStraight(List<ICard> allCards, out List<ICard> straightCards)
    {
        throw new NotImplementedException();
    }

    private bool IsRoyalFlush(List<ICard> cards, out List<ICard> royalFlushCards)
    {
        royalFlushCards = new List<ICard>();

        // Group cards by suit and find flushes
        var flushCards = cards
            .GroupBy(c => c.Suit)
            .Where(g => g.Count() >= 5)
            .SelectMany(g => g)
            .ToList();

        if (flushCards.Count < 5)
            return false;

        var royalFlushRanks = new List<Rank> { Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };

        // Check each suit group for a royal flush
        foreach (var suitGroup in flushCards.GroupBy(c => c.Suit))
        {
            var suitedCards = suitGroup.OrderByDescending(c => c.Rank).ToList();
            var suitedRanks = suitedCards.Select(c => c.Rank).ToList();

            // Check if all royal flush ranks are present
            if (royalFlushRanks.All(r => suitedRanks.Contains(r)))
            {
                // Create a list of royal flush cards
                royalFlushCards = suitedCards.Where(c => royalFlushRanks.Contains(c.Rank)).ToList<ICard>(); // Ensure the result is List<ICard> directly

                return true;
            }
        }

        return false;
    }

    public bool IsStraightFlush(List<ICard> cards, out List<ICard> straightFlushCards)
    {
        straightFlushCards = new List<ICard>();
        var flushCards = cards.GroupBy(c => c.Suit).Where(g => g.Count() >= 5).SelectMany(g => g).ToList();
        if (flushCards.Count < 5)
            return false;

        if (IsStraight(flushCards, out straightFlushCards))
            return true;

        return false;
    }

    public bool IsFourOfAKind(List<ICard> cards, out List<ICard> fourOfAKindCards)
    {
        fourOfAKindCards = new List<ICard>();
        var rankGroups = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 4).ToList();

        if (rankGroups.Any())
        {
            fourOfAKindCards = rankGroups.SelectMany(g => g).ToList();
            return true;
        }

        return false;
    }

    private bool IsFullHouse(List<ICard> cards, out List<ICard> fullHouseCards)
    {
        fullHouseCards = new List<ICard>();
        var threeOfAKind = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 3).ToList();
        var pairs = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 2).ToList();

        if (threeOfAKind.Any() && pairs.Any())
        {
            fullHouseCards = threeOfAKind.SelectMany(g => g).Take(3).Concat(pairs.SelectMany(g => g).Take(2)).ToList();
            return true;
        }

        return false;
    }

    private bool IsFlush(List<ICard> cards, out List<ICard> flushCards)
    {
        flushCards = new List<ICard>();
        var flushGroup = cards.GroupBy(c => c.Suit).FirstOrDefault(g => g.Count() >= 5);

        if (flushGroup != null)
        {
            flushCards = flushGroup.OrderByDescending(c => c.Rank).Take(5).ToList();
            return true;
        }

        return false;
    }

    private bool IsStraight(List<Card> cards, out List<ICard> straightCards)
    {
        straightCards = new List<ICard>();
        var orderedCards = cards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Distinct().ToList();

        for (int i = 0; i <= orderedCards.Count - 5; i++)
        {
            if (orderedCards[i] - orderedCards[i + 4] == 4)
            {
                straightCards = orderedCards.Skip(i).Take(5).Select(r => (ICard)new Card(0, r, cards.First(c => c.Rank == r).Suit)).ToList();
                return true;
            }
        }

        // Special case for Ace-low straight
        if (orderedCards.Contains(Rank.Ace) && orderedCards.TakeLast(4).SequenceEqual(new List<Rank> { Rank.Five, Rank.Four, Rank.Three, Rank.Two }))
        {
            straightCards = new List<ICard>
                {
                    new Card(0, Rank.Five, cards.First(c => c.Rank == Rank.Five).Suit),
                    new Card(0, Rank.Four, cards.First(c => c.Rank == Rank.Four).Suit),
                    new Card(0, Rank.Three, cards.First(c => c.Rank == Rank.Three).Suit),
                    new Card(0, Rank.Two, cards.First(c => c.Rank == Rank.Two).Suit),
                    new Card(0, Rank.Ace, cards.First(c => c.Rank == Rank.Ace).Suit)
                };
            return true;
        }

        return false;
    }

    private bool IsThreeOfAKind(List<ICard> cards, out List<ICard> threeOfAKindCards)
    {
        threeOfAKindCards = new List<ICard>();
        var rankGroup = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 3);

        if (rankGroup != null)
        {
            threeOfAKindCards = rankGroup.ToList();
            return true;
        }

        return false;
    }

    private bool IsTwoPair(List<ICard> cards, out List<ICard> twoPairCards)
    {
        twoPairCards = new List<ICard>();
        var pairs = cards.GroupBy(c => c.Rank).Where(g => g.Count() == 2).ToList();

        if (pairs.Count >= 2)
        {
            twoPairCards = pairs.Take(2).SelectMany(g => g).ToList();
            return true;
        }

        return false;
    }

    private bool IsOnePair(List<ICard> cards, out List<ICard> onePairCards)
    {
        onePairCards = new List<ICard>();
        var pair = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 2);

        if (pair != null)
        {
            onePairCards = pair.ToList();
            return true;
        }

        return false;
    }
}

