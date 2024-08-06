namespace Poker
{
    public class Table : ITable
    {
        public int Id { get; private set; }
        public int MinimumBuyIn { get; private set; }
        private readonly List<ICard> _communityCards;
        private decimal _pot;

        public Table(int id, int minimumBuyIn)
        {
            Id = id;
            MinimumBuyIn = minimumBuyIn;
            _communityCards = new List<ICard>(5); // Preallocate space for community cards
            _pot = 0;
        }

        public IReadOnlyList<ICard> CommunityCards => _communityCards.AsReadOnly();
        public decimal Pot => _pot;

        public Table AddCardToCommunity(ICard card)
        {
			
            _communityCards.Add(card);
            return this;
        }

        public Table AddToPot(decimal amount)
        {
            _pot += amount;
            return this;
        }

        public Table ClearCommunityCards()
        {
            _communityCards.Clear();
            return this;
        }

        public Table ClearPot()
        {
            _pot = 0;
            return this;
        }
    }
}
