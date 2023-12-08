using Godot;
using System;

public partial class InteractRifle : Node
{
	private MainPlayerController player;

	private float gunCharge = 5; //seconds
	
	public override void _Ready()
	{
		player = (MainPlayerController) FindParent("ControlablePlayer");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print("Current charge: " + gunCharge);
		bool isShooting = Input.IsActionPressed("player_shoot");
		bool isReloading = Input.IsActionPressed("player_reload");

		if (isReloading)
		{
			gunCharge += (float) delta * 2f; // 2s of shooting time per second reloading
		}

		if (isShooting)
		{
			gunCharge -= (float)delta;
			doShoot();
		}
	}

	private void doShoot()
	{
		throw new NotImplementedException();
	}
}
