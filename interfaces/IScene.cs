namespace GameEngine.interfaces;

/// <summary>
/// Интерфейс игровой сцены. Поддерживает загрузку и обновление.
/// </summary>
public interface IScene
{
    void Load();
    void Update();
}