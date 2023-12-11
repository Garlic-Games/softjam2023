using Godot;

namespace Softjam2023.Scripts.StateMachine;

public partial class GameTime : Node {
    
    public float Time { get; private set; } = 0f;

    private bool _countTime = false;
    public override void _Process(double delta)
    {
        if (_countTime) {
            Time += (float) delta;
        }
    }
    
    public void GameStateChange(int newState, int oldState)
    {
        switch ((GameStates) newState) {
            case GameStates.Intro:
            case GameStates.Calm:
            case GameStates.Warning:
            case GameStates.Danger:
                _countTime = true;
                break;
            default:
                _countTime = false;
                break;
            
        }
    }
}