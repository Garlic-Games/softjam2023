using Godot;
using System;

public partial class Rifle : Node3D
{
	[Export] public Node3D barrelEndPoint;
	[Export] public Node3D barrelStartPoint;
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
