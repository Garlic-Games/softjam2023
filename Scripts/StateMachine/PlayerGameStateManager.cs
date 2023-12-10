using Godot;
using System;

public partial class PlayerGameStateManager : Node
{
    [Export]
    private PlayerCamera _camera;

    public void StateChange(GameStates newState, GameStates oldState)
    {
        switch (newState)
        {
            case GameStates.Intro:
            case GameStates.Calm:
                _camera.SetCalm();
                break;
            case GameStates.Warning:
                _camera.SetWarning();
                break;
            case GameStates.Danger:
                _camera.SetDanger();
                break;
        }
    }
}
