using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Enums;

namespace Poker
{
    public class OnHand
    {
        private List<ICard> _cards;
        private const int MaxCards = 2;

        public OnHand()
        {
            _cards = new List<ICard>(MaxCards);
        }

        public OnHand AddCard(ICard card)
        {
            if (_cards.Count >= MaxCards)
            {
                throw new InvalidOperationException("A hand can only contain two cards.");
            }
            _cards.Add(card);
            return this;
        }

        public OnHand Clear()
        {
            _cards.Clear();
            return this;
        }

        public IReadOnlyList<ICard> GetCards()
        {
            return _cards.AsReadOnly();
        }

        public IEnumerable<ICard> BestHand(IEnumerable<ICard> communityCards)
        {
            var allCards = _cards.Concat(communityCards).ToList();
            var bestHand = new List<ICard>();
            var bestRank = HandRanking.HighCard;

            // Evaluate all combinations of 5 cards from the total available cards
            var allCombinations = GetCombinations(allCards, 5);

            foreach (var combination in allCombinations)
            {
                var (handRank, handCards) = EvaluateHand(combination);
                if (handRank > bestRank)
                {
                    bestRank = handRank;
                    bestHand = handCards.ToList();
                }
            }

            return bestHand;
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

        private (HandRanking, IEnumerable<ICard>) EvaluateHand(IEnumerable<ICard> cards)
        {
            var allCards = cards.ToList();

            // Determine the hand rank
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

            // If no hand matched, return HighCard
            return (HandRanking.HighCard, allCards.OrderByDescending(c => c.Rank).Take(5));
        }

        private bool IsRoyalFlush(List<ICard> cards, out List<ICard> royalFlushCards)
        {
            royalFlushCards = new List<ICard>();

            var flushCards = cards
                .GroupBy(c => c.Suit)
                .Where(g => g.Count() >= 5)
                .SelectMany(g => g)
                .ToList();

            if (flushCards.Count < 5)
                return false;

            var royalFlushRanks = new List<Rank> { Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };

            foreach (var suitGroup in flushCards.GroupBy(c => c.Suit))
            {
                var suitedCards = suitGroup.OrderByDescending(c => c.Rank).ToList();
                var suitedRanks = suitedCards.Select(c => c.Rank).ToList();

                if (royalFlushRanks.All(r => suitedRanks.Contains(r)))
                {
                    royalFlushCards = suitedCards.Where(c => royalFlushRanks.Contains(c.Rank)).ToList();
                    return true;
                }
            }

            return false;
        }

        private bool IsStraightFlush(List<ICard> cards, out List<ICard> straightFlushCards)
        {
            straightFlushCards = new List<ICard>();
            var flushCards = cards.GroupBy(c => c.Suit).Where(g => g.Count() >= 5).SelectMany(g => g).ToList();
            if (flushCards.Count < 5)
                return false;

            if (IsStraight(flushCards, out straightFlushCards))
                return true;

            return false;
        }

        private bool IsFourOfAKind(List<ICard> cards, out List<ICard> fourOfAKindCards)
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

        private bool IsStraight(List<ICard> cards, out List<ICard> straightCards)
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

            if (pairs.Count() >= 2)
            {
                twoPairCards = pairs.SelectMany(g => g).ToList();
                return true;
            }

            return false;
        }

        private bool IsOnePair(List<ICard> cards, out List<ICard> onePairCards)
        {
            onePairCards = new List<ICard>();
            var pairGroup = cards.GroupBy(c => c.Rank).FirstOrDefault(g => g.Count() == 2);

            if (pairGroup != null)
            {
                onePairCards = pairGroup.ToList();
                return true;
            }

            return false;
        }
    }
}