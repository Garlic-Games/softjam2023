using Godot;
using System;

public partial class AimRifle : Node
{

    private MainPlayerController player;
    private Node3D rifle;

    public override void _Ready()
    {
        player = GetParent<MainPlayerController>();
        rifle = (Node3D) FindChild("rifle");
    }
    public override void _PhysicsProcess(double delta)
    {
        // rifle.
    }
}
