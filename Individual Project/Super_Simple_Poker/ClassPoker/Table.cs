namespace SuperSimplePoker;

public class Table
    {
        public List<Card> CommunityCards { get; private set; }
        public int Pot { get; private set; }

        public Table()
        {
            CommunityCards = new List<Card>();
            Pot = 0;
        }

        public void AddToPot(int amount)
        {
            Pot += amount;
        }

        public void ClearPot()
        {
            Pot = 0;
        }

        public void AddCommunityCard(Card card)
        {
            CommunityCards.Add(card);
        }

        public void ClearCommunityCards()
        {
            CommunityCards.Clear();
        }
    }

