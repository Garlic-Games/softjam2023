using Godot;
using System;
using Softjam2023.Scripts.Autoload;

public partial class InteractRifle : Node
{
	private MainPlayerController player;
	private Rifle rifle;
	private ObjectInstanceProviderAutoLoad _autoLoad;

	private float gunCharge = 5; //seconds
	
	private float _spawn_speed = 0.15f;
	private float _cooldown = 0f;
	
	public override void _Ready()
	{
		player = (MainPlayerController) FindParent("ControlablePlayer");
		rifle = player.rifle;
		_autoLoad = GetNode<ObjectInstanceProviderAutoLoad>("/root/ObjectInstanceProviderAutoLoad");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// GD.Print("Current charge: " + gunCharge);
		bool isShooting = Input.IsActionPressed("player_shoot");
		bool isReloading = Input.IsActionPressed("player_reload");

		if (isReloading)
		{
			gunCharge += (float) delta * 2f; // 2s of shooting time per second reloading
		}

		if (isShooting)
		{
			gunCharge -= (float)delta;
			if (_cooldown < 0)
			{
				doShoot();
				_cooldown = _spawn_speed;
			}

			_cooldown -= (float) delta;
		}
		else
		{
			_cooldown = 0;
		}
	}

	private void doShoot()
	{
		// var projectile = _autoLoad.GimmeAWaterProjectile();
		var projectile = _autoLoad.GimmeATestProjectile();
		projectile.Shoot();
		var newTransform  = rifle.muzzlePoint.GlobalTransform;
		projectile.GlobalTransform = newTransform;
		projectile.LookAt(rifle.muzzlePoint.GlobalPosition);
	}
}
