using Godot;

namespace Softjam2023.Scripts.Player;

public partial class WaterProjectile : CharacterBody3D
{
    public enum Status
    {
        HIDDEN,
        IDLE,
        MOVING
    };

    private Status status = Status.IDLE;
    private float Speed = 50f;
    public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    
    
    public override void _PhysicsProcess(double delta)
    {
        if (status == Status.MOVING)
        {
            Vector3 velocity = Velocity;
            velocity.Y -= gravity * (float)delta;
            // velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
	        Vector3 direction = (Transform.Basis * new Vector3(0, 1, 1)).Normalized();
            velocity.X = direction.X * Speed;
            velocity.Z = direction.Z * Speed;
            // velocity.X = 20f;
            // velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
            Velocity = velocity;
            MoveAndSlide();
        }

    }

    public void Shoot()
    {
        status = Status.MOVING;
    }
}