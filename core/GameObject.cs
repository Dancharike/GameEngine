using GameEngine.interfaces;
using GameEngine.utility;

namespace GameEngine.core;

public class GameObject : IRender
{
    public string Tag { get; set; }
    public ICollider Collider { get; set; }
    public IVisual Visual { get; set; }

    public GameObject(string tag, ICollider collider, IVisual visual)
    {
        Tag = tag;
        Collider = collider;
        Visual = visual;
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
    
    public void Render(Graphics g)
    {
        Visual?.Render(g, Collider.Position, Collider.Size);
    }
}