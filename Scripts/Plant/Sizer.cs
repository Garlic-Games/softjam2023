using Godot;
using System;

public partial class Sizer : Node3D
{
	private Node3D _parent;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		_parent = GetParent<Node3D>();
	}

    public void Grow(float growthRate)
    {
        _parent.Scale += Vector3.Up * growthRate;
    }
}
