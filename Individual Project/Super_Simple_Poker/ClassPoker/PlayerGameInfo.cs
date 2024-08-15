namespace SuperSimplePoker;
using SuperSimplePoker;

public class PlayerGameInfo
{
    public Player Player { get; private set; }
    public List<Card> UnsortedHand { get; set; } // Revert back to UnsortedHand
    public int Money { get; set; }
    public int Bet { get; set; }
    public bool PlayerIngame { get; set; }
    public HandEvaluator HandEvaluator { get; set; }

    public PlayerGameInfo(Player player)
    {
        Player = player;
        UnsortedHand = new List<Card>();
        Money = 0;
        Bet = 0;
        PlayerIngame = true;
        HandEvaluator = null;
    }

    public void AddCard(Card card)
    {
        if (UnsortedHand.Count >= 2)
        {
            throw new InvalidOperationException("Cannot add more cards, hand is full.");
        }
        UnsortedHand.Add(card);
    }

    public void ClearHand()
    {
        UnsortedHand.Clear();
        HandEvaluator = null;
    }
}



