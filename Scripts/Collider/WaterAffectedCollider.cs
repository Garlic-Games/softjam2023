using Godot;

public partial class WaterAffectedCollider : RigidBody3D {
    [Signal]
    public delegate void WaterCollidedEventHandler();

    public void NotifyWaterCollision() {
        EmitSignal(SignalName.WaterCollided);
    }
}