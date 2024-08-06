namespace Poker
{
    public class Player : IPlayer
    {
        private static int _idCounter = 1;

        public int Id { get; private set; }
        public string Name { get; set; }

        public Player(string name)
        {
            Id = _idCounter++;
            Name = name;
        }
    }
}
