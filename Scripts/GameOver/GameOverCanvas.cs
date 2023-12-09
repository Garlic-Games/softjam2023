using Godot;
using System;

public partial class GameOverCanvas : CanvasLayer
{
	[Export]
	private StateMachine _machine;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_machine.StateChanged += Show;
	}

	public void Show(int newState, int oldState)
	{
		switch((GameStates)newState)
		{
			case GameStates.GameOver:
                Input.MouseMode = Input.MouseModeEnum.Visible;
                Show();
				break;
        }
    }
}
