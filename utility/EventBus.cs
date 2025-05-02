using GameEngine.interfaces;

namespace GameEngine.utility;

public class EventBus
{
    private static readonly Dictionary<Type, List<Action<IEvent>>> _subscribers = new();

    public static void Subscribe<T>(Action<T> handler) where T : IEvent
    {
        var type = typeof(T);

        if (!_subscribers.ContainsKey(type))
        {
            _subscribers[type] = new List<Action<IEvent>>();
        }

        _subscribers[type].Add(e => handler((T)e));
    }

    public static void Publish<T>(T evt) where T : IEvent
    {
        var type = typeof(T);

        if (_subscribers.ContainsKey(type))
        {
            var handlers = _subscribers[type].ToList();
            
            foreach (var handler in handlers)
            {
                handler.Invoke(evt);
            }
        }
    }

    public static void ClearAll()
    {
        _subscribers.Clear();
    }
}