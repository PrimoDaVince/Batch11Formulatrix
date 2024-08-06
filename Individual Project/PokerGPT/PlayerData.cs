using Poker.Enums;

namespace Poker
{
    public class PlayerData
    {
        public Player Player { get; private set; }
        public decimal Money { get; set; }
        public decimal Chips { get; set; }
        public OnHand Hand { get; private set; }
        public decimal CurrentBet { get; set; }
        public PlayerStatus Status { get; set; }

        public PlayerData(Player player, decimal initialMoney = 0, decimal initialChips = 0)
        {
            Player = player;
            Money = initialMoney;
            Chips = initialChips;
            Hand = new OnHand();
            CurrentBet = 0;
            Status = PlayerStatus.Active;
        }

        public void UpdateBet(decimal amount)
        {
            Chips -= amount;
            CurrentBet = amount;
        }

        public PlayerData Fold()
        {
            Status = PlayerStatus.Folded;
            return this;
        }

        public PlayerData ResetStatus()
        {
            Status = PlayerStatus.Active;
            CurrentBet = 0;
            return this;
        }
    }
}
