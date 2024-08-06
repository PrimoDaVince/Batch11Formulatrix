namespace Poker
{
    public interface ITable
    {
        int Id { get; }
        int MinimumBuyIn { get; }
        IReadOnlyList<ICard> CommunityCards { get; }
        decimal Pot { get; }
        Table AddCardToCommunity(ICard card);
        Table AddToPot(decimal amount);
        Table ClearCommunityCards();
        Table ClearPot();
    }
}
