namespace Goat.Domain
{
    internal struct Participant
    {
        public readonly GamePlayer Player;
        public readonly int Order;

        public Participant(GamePlayer player, int order)
        {
            Player = player;
            Order = order;
        }
    }
}
