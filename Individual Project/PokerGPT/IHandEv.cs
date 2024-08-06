using System.Collections.Generic;
using Poker.Enums;

namespace Poker
{
    public interface IHandEvaluator
    {
        HandRanking EvaluateBestHand(IEnumerable<ICard> allCards, out List<ICard> handCards);
    }
}
