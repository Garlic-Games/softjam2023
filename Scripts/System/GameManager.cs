using Godot;
using System;

public partial class GameManager : Node3D {

	[Export]
	private float _firstPanelTime = 3f;

	[Export]
	private float _secondPanelTime = 7f;

	[Export]
	private Node3D _gameContainer;

	[Export]
	private Panel _firstPanelToHide;

	[Export]
	private Panel _secondPanelToHide;

	private float _count;
	private bool _loaded = false;
	private Node _game = null;

	public override void _Ready() {
		_firstPanelToHide.Visible = true;
	}

	public override void _Process(double delta) {
		if (_loaded) {
			return;
		}
		_count += (float) delta;
		if (_count > _firstPanelTime) {
			_firstPanelToHide.Visible = false;
			_secondPanelToHide.Visible = true;
		}
		if (_count > (_firstPanelTime + _secondPanelTime)) {
			_secondPanelToHide.Visible = false;
			LoadGame();
		}
		
	}

	public void LoadGame() {
		if (_loaded) {
			return;
		}
		_loaded = true;
		string path = "res://Scenes/world_tomeu.tscn";

		if (ResourceLoader.Load(path) is PackedScene scene) {
			_game = scene.Instantiate();
			_gameContainer.AddChild(_game);
		}
	}

	public void RestartGame() {
		if (_game != null) {
			var currentScene = GetTree().CurrentScene.SceneFilePath;
			// print(currentScene) # for Debug
			GetTree().ChangeSceneToFile(currentScene);
		}
	}
	
}
