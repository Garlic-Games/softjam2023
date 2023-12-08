using Godot;
using System;

public enum GameStates { Intro = 0, Calm = 1, Warning = 2, Danger = 3, GameOver = 4 }

public partial class StateMachine : Node
{
	[Signal]
	public delegate void StateChangedEventHandler(int state);

    public GameStates State { get; private set; } = GameStates.GameOver;

    public override void _Ready()
    {
        ChangeState(GameStates.Intro);
    }

    public void ChangeState(GameStates newState)
	{
		if(State == newState) return;
		
		switch(newState)
		{
			case GameStates.Intro:
				MusicManager.Instance.PlayIntro(); 
				break;
            case GameStates.Calm:
                MusicManager.Instance.PlayIntro();
                break;
            case GameStates.Warning:
                MusicManager.Instance.PlayIntro();
                break;
            case GameStates.Danger:
                MusicManager.Instance.PlayIntro();
                break;
        }
        State = newState;
		
		EmitSignal(SignalName.StateChanged, (int)newState);
	}
}
