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
    public delegate void StateChangedEventHandler(int state, int previousState);

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
        if(_plant != null)
        {
            _plant.PlantDead += LooseGame;
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

    private void LooseGame()
    {
        ChangeState(GameStates.GameOver);
    }

    public void ChangeState(GameStates newState)
    {
        if (State == newState) return;
        GameStates oldState = newState;
        State = newState;

        EmitSignal(SignalName.StateChanged, (int)newState, (int)oldState);
    }
}
