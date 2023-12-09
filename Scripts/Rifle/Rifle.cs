using Godot;
using System;

public partial class Rifle : Node3D
{
	[Export] public Node3D barrelEndPoint;
	[Export] public Node3D barrelStartPoint;

	[Export]
	public MeshInstance3D rilfeMesh;

	
	private Material _defaultGunMaterial;
	private Material _defaultGunMaterialNoShine;

	private const float ShineInterval = 0.3f;
	private float _shineIntervalCount = 0;
	private bool _shining = true;
	
	private bool _shouldShine = false;
	
	public override void _Ready() {
		_defaultGunMaterial = GD.Load("res://Art/rifle/Default_Material.tres") as Material;
		_defaultGunMaterialNoShine = GD.Load("res://Art/rifle/Default_Material_no_glow.tres") as Material;
	}

	
	public override void _Process(double delta) {


		if (_shouldShine) {
			_shineIntervalCount += (float) delta;

			if (_shineIntervalCount > ShineInterval) {
				_shineIntervalCount = 0f;
				_shining = !_shining;
				ShineChange(_shining);
			}
		}
	}

	private void ShineChange(bool shine) {
		if (shine) {
			rilfeMesh.SetSurfaceOverrideMaterial(0, _defaultGunMaterial);
		} else {
			rilfeMesh.SetSurfaceOverrideMaterial(0, _defaultGunMaterialNoShine);
		}
	}

	public bool ShouldShine {
		get {
			return _shouldShine;
		}
		set {
			if (value != _shouldShine) {
				ShineChange(!value);
			}

			_shouldShine = value;
		}
	}
}
