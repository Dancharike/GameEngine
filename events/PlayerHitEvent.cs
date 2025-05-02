using GameEngine.interfaces;

namespace GameEngine.events;

public class PlayerHitEvent : IEvent
{
    public int Damage { get; set; }

    public PlayerHitEvent(int damage)
    {
        Damage = damage;
    }
}