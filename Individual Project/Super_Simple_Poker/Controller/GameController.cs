using SuperSimplePoker;
using NLog;
public class GameController
{
    private readonly DeckOfCards _deck;
    private readonly Table _table;
    private readonly List<PlayerGameInfo> _players;
    private readonly ILogger _log;

    public GameController(DeckOfCards deck, Table table, ILogger log)
    {
        _deck = deck;
        _table = table;
        _log = log;
        _players = new List<PlayerGameInfo>();
    }

    public void InitializePlayer(Player player, int moneyPerPlayer)
    {
        PlayerGameInfo playerGameInfo = new PlayerGameInfo(player)
        {
            Money = moneyPerPlayer,
            PlayerIngame = true
        };
        playerGameInfo.UnsortedHand.Add(_deck.DealCard());
        playerGameInfo.UnsortedHand.Add(_deck.DealCard());

        playerGameInfo.HandEvaluator = new HandEvaluator(playerGameInfo.UnsortedHand);
        _players.Add(playerGameInfo);
        _log.Info($"Initialized Player: {player.Name}");
    }

    public List<Card> GetAllCards()
    {
        return _deck.GetAllCards();
    }

    public List<PlayerGameInfo> GetPlayers()
    {
        return _players;
    }

    public void DealCommunityCards(int count)
    {
        for (int i = 0; i < count; i++)
        {
            _table.AddCommunityCard(_deck.DealCard());
        }
    }

    public List<Card> GetCommunityCards()
    {
        return _table.CommunityCards;
    }

    public void ClearCommunityCards()
    {
        _table.ClearCommunityCards();
    }

    public int GetPot()
    {
        return _table.Pot;
    }

    public void AddToPot(int amount)
    {
        _table.AddToPot(amount);
    }

    public void ClearPot()
    {
        _table.ClearPot();
    }

    public void RemovePlayersWithoutMoney()
    {
        _players.RemoveAll(player => player.Money <= 0);
    }
}