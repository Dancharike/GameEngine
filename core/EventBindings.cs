using GameEngine.events;
using GameEngine.utility;

namespace GameEngine.core;

/// <summary>
/// Класс содержащий в себе все пред созданные события в движке.
/// </summary>
public class EventBindings
{
    public static void RegisterEvents()
    {
        EventBus.Subscribe<PlayerHitEvent>(e =>
        {
            Log.Event($"Player is hit for {e.Damage} damage.");
        });
        
    }
}