namespace Poker
{
    public class Position
    {
        public int Index { get; private set; }

        public Position(int index)
        {
            Index = index;
        }

        public Position Rotate(int totalPlayers)
        {
            Index = (Index + 1) % totalPlayers;
            return this;
        }
    }
}
