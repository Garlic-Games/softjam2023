using Godot;
using System;

public partial class GameManager : Node3D {

	[Export]
	private float _waitUntilLoad = 7f;

	[Export]
	private Node3D _gameContainer;

	[Export]
	private CanvasLayer _elementToHideOnStart;

	private float _count;
	private bool _loaded = false;
	private Node _game = null;

	public override void _Process(double delta) {
		if (_loaded) {
			return;
		}
		_count += (float) delta;
		if (_count > _waitUntilLoad) {
			_elementToHideOnStart.Visible = false;
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
		_loaded = false;
		if (_game != null) {
			_game.GetParent().RemoveChild(_game);
			QueueFree();
			LoadGame();
		}
	}
	
}
