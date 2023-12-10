using Godot;
using System;
using System.Diagnostics;

public partial class AimRifle : Node
{

    private MainPlayerController player;

    public override void _Ready()
    {
        player = (MainPlayerController) FindParent("ControlablePlayer");
        
    }
    public override void _PhysicsProcess(double delta)
    {
        player.rifleContainer.LookAt(player.aimPoint.GlobalPosition);
    }
}
