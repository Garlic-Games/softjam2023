using Godot;
using System;
using Softjam2023.Scripts.Autoload;
using Softjam2023.Scripts.Util;

public partial class WaterAffectedCollider : RigidBody3D {

	private float _tempCount = 0f;

	private float _ratio;

	private TempShower _tempShower;

	public override void _Ready() {
		_ratio = GD.Randf() * 2 + 1;
		if (Constants.DebugMode) {
			ObjectInstanceProviderAutoLoad autoLoad = 
				GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
			_tempShower = autoLoad.GimmeAProgressBarTempShower();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		if (_tempCount < 100f) {
			_tempCount += _ratio * (float) delta;
		}

		if (_tempShower != null) {
			Camera3D camera = GetViewport().GetCamera3D();
			Vector3 position = GlobalPosition;
			if (camera != null ) {
				if (!camera.IsPositionBehind(position)) {
					Vector2 position2d = camera.UnprojectPosition(GlobalPosition);
					_tempShower.Show();
					_tempShower.SetPosition(position2d);
					_tempShower.SetBarPercent(_tempCount);
				} else {
					_tempShower.Hide();
				}

			}
		}

	}
}
