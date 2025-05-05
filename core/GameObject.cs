using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.core;

public class GameObject : IRender
{
    public string Tag { get; set; }
    public ICollider Collider { get; set; }
    
    private ISprite _sprite;

    public GameObject(string tag, ICollider collider, ISprite sprite)
    {
        Tag = tag;
        Collider = collider;
        _sprite = sprite;
    }

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

    public void SetSprite(ISprite sprite)
    {
        _sprite = sprite;
    }
    
    public void Render(Graphics g)
    {
        _sprite?.Draw(g, Collider.Position, Collider.Size);
    }
}