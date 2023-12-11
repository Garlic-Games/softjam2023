using Godot;
using System;

public partial class Leaf : MeshInstance3D
{

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        Scale = Vector3.One * GlobalPosition.Y / 100;
	}
}
