using GameEngine.components;

namespace GameEngine.interfaces;

public interface ICameraProvider
{
    CameraComponent GetCamera();
}