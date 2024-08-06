namespace Poker
{
    public interface ITable
    {
        int Id { get; }
        int MinimumBuyIn { get; }
        IReadOnlyList<ICard> CommunityCards { get; }
        decimal Pot { get; }
    }
}
