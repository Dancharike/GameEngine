using GameEngine.interfaces;
using GameEngine.visuals;

namespace GameEngine.utility;

/// <summary>
/// Менеджер для загрузки и кэширования спрайтов.
/// Поддерживает спрайты и анимированные спрайты из одного метода.
/// </summary>
public static class SpriteLoader
{
    private static readonly Dictionary<string, ISprite> _cache = new();
    private static readonly string[] _extensions = { ".png", ".jpg", ".jpeg", ".aseprite" };
    private const string _defaultAssetFolder = "assets";

    /// <summary>
    /// Загружает обычный или анимированный спрайт из одного изображения (sprite sheet).
    /// </summary>
    /// <param name="spriteName">Имя файла без расширения</param>
    /// <param name="frameCount">Количество кадров по горизонтали (если больше 1 — будет анимация)</param>
    /// <param name="framesPerSecond">Скорость анимации в кадрах в секунду</param>
    /// <returns>ISprite — обычный или анимированный спрайт</returns>
    public static ISprite LoadSprite(string spriteName, int frameCount = 1, int framesPerSecond = 30)
    {
        string cacheKey = $"{spriteName}_f{frameCount}_fps{framesPerSecond}";

        if (_cache.TryGetValue(cacheKey, out ISprite cached))
        {
            return cached;
        }

        string? path = FindImagePath(spriteName);
        if (path == null)
        {
            throw new FileNotFoundException($"Sprite '{spriteName}' not found in '{_defaultAssetFolder}'");
        }

        Image image = Image.FromFile(path);

        ISprite sprite = (frameCount <= 1) ? new Sprite(image) : new AnimatedSprite(image, frameCount, framesPerSecond);

        _cache[cacheKey] = sprite;
        return sprite;
    }

    /// <summary>
    /// Загружает анимацию из набора отдельных файлов (кадров).
    /// Например: spr_idle_1.png, spr_idle_2.png, ..., spr_idle_N.png
    /// </summary>
    /// <param name="baseName">Имя спрайта без номера и расширения</param>
    /// <param name="frameCount">Общее количество кадров</param>
    /// <param name="framesPerSecond">Скорость анимации в кадрах в секунду</param>
    /// <returns>AnimatedSprite</returns>
    public static AnimatedSprite LoadSpriteFromFrames(string baseName, int frameCount, int framesPerSecond = 10)
    {
        string cacheKey = $"{baseName}_frames_{frameCount}_fps{framesPerSecond}";
        if (_cache.TryGetValue(cacheKey, out ISprite cached))
        {
            return (AnimatedSprite)cached;
        }

        List<Image> frames = new();

        for (int i = 1; i <= frameCount; i++)
        {
            string? path = FindImagePath($"{baseName}_{i}");

            if (path == null)
            {
                throw new FileNotFoundException($"Frame '{baseName}_{i}' not found");
            }

            frames.Add(Image.FromFile(path));
        }

        var animated = new AnimatedSprite(frames, framesPerSecond);
        _cache[cacheKey] = animated;
        return animated;
    }

    /// <summary>
    /// Ищет путь к изображению с поддерживаемым расширением.
    /// </summary>
    /// <param name="name">Имя файла без расширения</param>
    /// <returns>Полный путь к изображению или null, если не найден</returns>
    private static string? FindImagePath(string name)
    {
        string outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _defaultAssetFolder);

        foreach (var ext in _extensions)
        {
            string outputPath = Path.Combine(outputDir, name + ext);
            if (File.Exists(outputPath))
            {
                return outputPath;
            }

            // попробуем найти в папке проекта
            string? projectBase = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?
                .Parent?.Parent?.Parent?.FullName;

            if (projectBase != null)
            {
                string projectAssetPath = Path.Combine(projectBase, _defaultAssetFolder, name + ext);

                if (File.Exists(projectAssetPath))
                {
                    // создание outputDir, если его нет
                    if (!Directory.Exists(outputDir))
                    {
                        Directory.CreateDirectory(outputDir);
                    }

                    // копирование
                    File.Copy(projectAssetPath, outputPath, true);
                    Console.WriteLine($"[SpriteLoader] auto copy: {name + ext} -> {outputDir}");

                    return outputPath;
                }
            }
        }

        return null;
    }
}
