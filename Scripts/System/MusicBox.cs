using Godot;
using System;

public partial class MusicBox : Node
{
	[Export]
	private StateMachine _stateMachine;

	public override void _Ready()
	{
        _stateMachine.StateChanged += ProcessState;
    }

    private void ProcessState(int state, int previousState)
    {
        GameStates newState = (GameStates)state;
        GameStates oldState = (GameStates)previousState;
        switch (newState)
        {
            case GameStates.Intro:
                MusicManager.Instance.PlayIntro();
                break;
            case GameStates.Calm:
                MusicManager.Instance.PlayCalmLoop();
                break;
            case GameStates.Warning:
                if (oldState == GameStates.Danger)
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
    }
}
