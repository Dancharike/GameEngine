using GameEngine;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        Engine engine = new Engine();
        DemoGame game = new DemoGame();

        // движок запускает сцену
        engine.Start(game, "2D test game", 1024, 768);
    }
}