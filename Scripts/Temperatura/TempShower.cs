using Godot;
using System;

public partial class TempShower : Node2D {

	private ProgressBar _progressBar;
	public override void _Ready() {
		_progressBar = GetChild<ProgressBar>(0);
	}


	public void SetBarPercent(float percent) {
		_progressBar.Value = percent;
	}

	public void SetPosition(Vector2 position) {
		_progressBar.Position = position;
	}
}
