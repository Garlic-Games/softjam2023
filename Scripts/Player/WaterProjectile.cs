using Godot;

namespace Softjam2023.Scripts.Player;

public partial class WaterProjectile : RigidBody3D
{
    public enum Status
    {
        HIDDEN,
        IDLE,
        MOVING
    };

    private const float TimeToLive = 2f; 
    
    private Status status = Status.IDLE;
    private float Speed = 50f;
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle() /4f;
    private Vector3 direction_forward = new Vector3(0, 0, -1);
    private Vector3 direction_down = new Vector3(0, -1, 0);

    private float _lifeSpan = 0f;


    public override void _Process(double delta) {
        if (_lifeSpan > TimeToLive) {
            GetParent().RemoveChild(this);
            QueueFree();
        }
    }

    public override void _PhysicsProcess(double delta)
    {

        if (status == Status.MOVING) {
            _lifeSpan += (float) delta;
            Translate(direction_forward.Normalized() * Speed * (float) delta);
            Translate(direction_down * gravity * (float) delta);
        }

    }

    public void Shoot()
    {
        status = Status.MOVING;
    }
    
    
}