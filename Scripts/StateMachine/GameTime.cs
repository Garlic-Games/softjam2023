using Godot;

namespace Softjam2023.Scripts.StateMachine;

public partial class GameTime : Node {
    
    [Signal]
    public delegate void GamePauseEventHandler(bool paused);

    public float Time { get; private set; } = 0f;

    private bool _countTime = false;
    private bool _gamePaused = true;

    public override void _Ready() {
        PauseGame();
    }

    public override void _Process(double delta) {
        if (_countTime) {
            Time += (float) delta;
        }

    }

    public override void _PhysicsProcess(double delta) {
        if (Input.IsActionJustPressed("ui_text_clear_carets_and_selection")) {
            if (_gamePaused) {
                UnPauseGame();
            } else {
                PauseGame();
            }

            _gamePaused = !_gamePaused;
        }
    }


    private void PauseGame() {
        GetTree().Paused = true;
        _countTime = false;
        Input.MouseMode = Input.MouseModeEnum.Visible;
        EmitSignal(SignalName.GamePause, true);
    }

    private void UnPauseGame() {
        GetTree().Paused = false;
        _countTime = true;
        Input.MouseMode = Input.MouseModeEnum.Captured;
        EmitSignal(SignalName.GamePause, false);
    }

    public void GameStateChange(int newState, int oldState) {
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