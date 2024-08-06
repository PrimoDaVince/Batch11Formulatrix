using System;
using System.Collections.Generic;
using System.Linq;
using Poker.Enums;

namespace Poker;

    public class HandRankComparator
    {
        public HandRanking DetermineBestHand(IEnumerable<ICard> playerHand, IEnumerable<ICard> communityCards)
        {
            var allCards = playerHand.Concat(communityCards).ToList();

            try
            {
                // Evaluate the best possible hand
                if (IsRoyalFlush(allCards, out List<ICard> royalFlushCards))
                    return HandRanking.RoyalFlush;
                if (IsStraightFlush(allCards, out List<ICard> straightFlushCards))
                    return HandRanking.StraightFlush;
                if (IsFourOfAKind(allCards, out List<ICard> fourOfAKindCards))
                    return HandRanking.FourOfAKind;
                if (IsFullHouse(allCards, out List<ICard> fullHouseCards))
                    return HandRanking.FullHouse;
                if (IsFlush(allCards, out List<ICard> flushCards))
                    return HandRanking.Flush;
                if (IsStraight(allCards, out List<ICard> straightCards))
                    return HandRanking.Straight;
                if (IsThreeOfAKind(allCards, out List<ICard> threeOfAKindCards))
                    return HandRanking.ThreeOfAKind;
                if (IsTwoPair(allCards, out List<ICard> twoPairCards))
                    return HandRanking.TwoPair;
                if (IsOnePair(allCards, out List<ICard> onePairCards))
                    return HandRanking.OnePair;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error determining best hand: {ex.Message}");
            }

            return HandRanking.HighCard;
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

        private bool IsStraight(List<Card> cards, out List<ICard> straightCards)
        {
            straightCards = new List<ICard>();
            var orderedCards = cards.OrderByDescending(c => c.Rank).Select(c => c.Rank).Distinct().ToList();

            for (int i = 0; i <= orderedCards.Count - 5; i++)
            {
                if (orderedCards[i] - orderedCards[i + 4] == 4)
                {
                   straightCards = orderedCards.Skip(i).Take(5).Select(r => (ICard)new Card(0, r,cards.First(c => c.Rank == r).Suit)).ToList();
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

