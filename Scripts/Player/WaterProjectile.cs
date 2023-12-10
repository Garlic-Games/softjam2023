using Godot;

namespace Softjam2023.Scripts.Player;

public partial class WaterProjectile : RigidBody3D {
    public enum Status {
        HIDDEN,
        IDLE,
        MOVING
    };

    private const float TimeToLive = 7f;

    private Status status = Status.IDLE;
    private float Speed = 50f;
    private float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
    private Vector3 direction_forward = new Vector3(0, 0, -1);
    private Vector3 direction_down = new Vector3(0, -1, 0);
    private Vector3 _customVelocity;

    private float _lifeSpan = 0f;
    private float _terminalVelocity = 53.0f;

    public override void _Ready() {
    }

    public override void _Process(double delta) {
        if (_lifeSpan > TimeToLive) {
            Explode();
        }
    }

    public override void _PhysicsProcess(double delta) {
        if (status == Status.MOVING) {
            _lifeSpan += (float) delta;
            if (_customVelocity.Y < _terminalVelocity) {
                _customVelocity.Y += gravity * (float) delta * -1;
            }
            KinematicCollision3D collission = MoveAndCollide((_customVelocity * (float) delta));
            if (collission != null) {
                GodotObject collidedWith = collission.GetCollider();
                Explode();
                if (collidedWith is WaterAffectedCollider waterCollider) {
                    GD.Print(waterCollider);
                }
            }
        }
    }
    
    public void Shoot() {
        status = Status.MOVING;
        //Thanks to: https://forum.godotengine.org/t/godot-3-rotation-doesnt-effect-axis-for-velocity/29980
        _customVelocity = GlobalTransform.Basis.Orthonormalized() * direction_forward * Speed;
    }

    private void Explode() {
        GetParent().RemoveChild(this);
        QueueFree();
    }
}