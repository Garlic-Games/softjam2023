using Godot;
using System;

public enum GameStates { Intro = 0, Calm = 1, Warning = 2, Danger = 3, GameOver = 4 }

public partial class StateMachine : Node
{
	[Signal]
	public delegate void StateChangedEventHandler(int state);

	public GameStates State { get; private set; }

	public void ChangeState(GameStates newState)
	{
		if(State == newState) return;
		
        State = newState;
		
		EmitSignal(SignalName.StateChanged, (int)newState);
	}
}
