using GameEngine.assets.player;
using GameEngine.components;
using GameEngine.core;
using GameEngine.events;
using GameEngine.graphics;
using GameEngine.interfaces;
using GameEngine.multi_thread;
using GameEngine.shapes;
using GameEngine.utility;
using GameEngine.window;

namespace GameEngine;

public class DemoGame : IScene, IRenderSource, ICameraProvider
{
    private List<GameObject> _objects = new();
    private ActivityAnalyzer _analyzer;
    private CameraComponent _camera;
    private Player _player;
    private WindowHost _window;
    private Renderer _renderer;

    public void SetWindow(WindowHost window)
    {
        _window = window;
    }

    public void SetRenderer(Renderer renderer)
    {
        _renderer = renderer;
    }
    
    public void Load()
    {
        _analyzer = new ActivityAnalyzer();
        _analyzer.Start();
        
        // здесь можно на разные кнопки с клавиатуры назначать разную логику кода
        EventBus.Subscribe<KeyPressedEvent>(e =>
        {
            if (e.Key == Keys.Escape)
            {
                Log.Event("Exiting the game.");
                Game.Exit();
                _analyzer.Stop();
            }

            if (e.Key == Keys.R)
            {
                Log.Event("Restarting the scene...");
                Game.SceneRestart();
            }

            if (e.Key == Keys.Z)
            {
                Log.Event("Restarting the game...");
                Game.GameRestart();
            }
            
        });
        
        var floorSprite = SpriteLoader.LoadSprite("spr_room_04");
        var floor = new GameObject("Floor", new BoxCollider(new Vector2(10, 10), floorSprite.GetSize()), floorSprite);
        _objects.Add(floor);
        
        _player = new Player(new Vector2(10, 10));
        _camera = new CameraComponent(_player, _window, new Vector2(480, 270)); // прикрепить к объекту, указать окно игры, указать размер окна камеры
        _renderer.SetCamera(_camera);
        _objects.Add(_player);
    }

    public void Update()
    {
        _camera.Update();
        
        foreach (var obj in _objects)
        {
            obj.Update();
        }
        
        KeyLogger.Flush();
    }

    public IEnumerable<IRender> GetRenderables()
    {
        return _objects;
    }

    public CameraComponent GetCamera()
    {
        return _camera;
    }
}