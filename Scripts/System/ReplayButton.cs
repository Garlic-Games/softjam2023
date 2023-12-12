using Godot;
using System;

public partial class ReplayButton : Button
{
	private GameManager _gameManager;

	public override void _Ready() {
		_gameManager = (GameManager) FindParent("StartMenu");
	}


	public override void _Pressed() {
		_gameManager.RestartGame();
	}
}
