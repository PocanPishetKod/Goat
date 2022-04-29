namespace Goat.Domain.Events
{
    public struct PlayerJoinedEvent
    {
        public readonly Player Player;

        public PlayerJoinedEvent(Player player)
        {
            Player = player;
        }
    }
}
