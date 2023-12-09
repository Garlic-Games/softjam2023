using Godot;
using System;

public enum GameStates { Intro = 0, Calm = 1, Warning = 2, Danger = 3, GameOver = 4 }

public partial class StateMachine : Node
{
    [Export]
    private Plant _plant;
    [Export]
    private Temperatura _temperatura;

    [Signal]
    public delegate void StateChangedEventHandler(int state);

    public GameStates State { get; private set; } = GameStates.GameOver;

    public override void _Ready()
    {
        ChangeState(GameStates.Intro);
        if (_temperatura != null)
        {
            _temperatura.CalmTemperatureReached += BeCalm;
            _temperatura.WarningTemperatureReached += BeNervous;
            _temperatura.DangerTemperatureReached += BeFreakedOut;
        }
    }

    private void BeFreakedOut()
    {
        ChangeState(GameStates.Danger);
    }

    private void BeNervous()
    {
        ChangeState(GameStates.Warning);
    }

    private void BeCalm()
    {
        ChangeState(GameStates.Calm);
    }

    public void ChangeState(GameStates newState)
    {
        if (State == newState) return;

        switch (newState)
        {
            case GameStates.Intro:
                MusicManager.Instance.PlayIntro();
                break;
            case GameStates.Calm:
                MusicManager.Instance.PlayCalmLoop();
                break;
            case GameStates.Warning:
                if (State == GameStates.Danger)
                {
                    MusicManager.Instance.PlayBackFromDanger();
                }
                else
                {
                    MusicManager.Instance.PlayWarning();
                }
                break;
            case GameStates.Danger:
                MusicManager.Instance.PlayDanger();
                break;
        }
        State = newState;

        EmitSignal(SignalName.StateChanged, (int)newState);
    }
}