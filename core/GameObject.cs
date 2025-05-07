using GameEngine.components;
using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.core;

/// <summary>
/// Базовый объект игры. Поддерживает события OnCreate, OnUpdate и OnDraw.
/// Все игровые объекты должны наследоваться от этого класса.
/// </summary>
public class GameObject : IRender, IUpdatable
{
    public string Tag { get; set; }
    public ICollider Collider { get; set; }
    
    private ISprite _sprite;
    
    public Vector2 Position
    {
        get => Collider.Position;
        set => Collider.Position = value;
    }

    public Vector2 Size
    {
        get => Collider.Size;
        set => Collider.Size = value;
    }
    
    public GameObject(string tag, ICollider collider, ISprite sprite)
    {
        Tag = tag;
        Collider = collider;
        _sprite = sprite;

        OnCreate();
    }

    public virtual void OnCreate() {}
    public virtual void OnUpdate() {}
    public virtual void OnDraw(Graphics g) {}

    public void Update()
    {
        OnUpdate();
    }
    
    public void Render(Graphics g)
    {
        _sprite?.Draw(g, Position, Size);
        OnDraw(g);
    }
}