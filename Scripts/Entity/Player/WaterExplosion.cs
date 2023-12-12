using Godot;
using System;

public partial class WaterExplosion : GpuParticles3D {

	[Export]
	private float _ttl = 1.5f;

	private float _timeCount = 0f;


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		_timeCount += (float) delta;
		if (_timeCount > _ttl) {
			Dissappear();
		}
	}

	private void Dissappear() {
		GetParent().RemoveChild(this);
		QueueFree();
	}
}
