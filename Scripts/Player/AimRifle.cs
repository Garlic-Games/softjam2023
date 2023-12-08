using Godot;
using System;
using System.Diagnostics;

public partial class AimRifle : Node
{

    private MainPlayerController player;
    private Node3D rifle;
    private Node3D aimPoint;

    public override void _Ready()
    {
        player = (MainPlayerController) FindParent("ControlablePlayer");
        rifle = player.GetNode<Node3D>("Visuals/rifle");
        aimPoint = player.GetNode<Node3D>("HeadTilt/AimPoint");
        // rifle = (Node3D) player.GetChild<Node3D>("rifle");
        // aimPoint = (Node3D) player.FindChild("AimPoint");

        GD.Print(rifle);
    }
    public override void _PhysicsProcess(double delta)
    {
        rifle.LookAt(aimPoint.GlobalPosition);
        // AimTo/
    }
}
