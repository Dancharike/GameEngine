using GameEngine.interfaces;

namespace GameEngine.events;

/// <summary>
/// Событийное присваивание разным кнопкам разной логики.
/// Можно выбрать любую кнопку и привязать к ней любую логику кода.
/// </summary>
public class KeyPressedEvent : IEvent
{
    public Keys Key { get; }

    public KeyPressedEvent(Keys key)
    {
        Key = key;
    }
}