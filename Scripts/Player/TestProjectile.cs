using Godot;

public partial class TestProjectile : Node3D

{
    public enum Status
    {
        HIDDEN,
        IDLE,
        MOVING
    };

    private float speed = 10f;

    private Status status = Status.IDLE;
    private Vector3 direction = new Vector3(0, 0, 1);


    public override void _Process(double delta)
    {
        // var basisX = GlobalTransform.Basis.X.X;
        // basisX -=  basisX * speed * (float) delta;
        // GlobalTransform.Basis.X -= GlobalTransform.Basis.X * (float) speed * delta;
        if (status == Status.MOVING)
        {
            Translate(direction.Normalized() * speed * (float) delta);
        }
    }


    public void Shoot()
    {
        status = Status.MOVING;
    }
}