using Godot;

public partial class HeadRotation : Node3D
{
    [Export] private float camera_sens = 0.7f;

    private Vector2 look_dir;
    private bool mouse_captured;

    private MainPlayerController player;

    private float xRotation = 0f;

    public override void _Ready()
    {
        player = GetParent<MainPlayerController>();
        capture_mouse();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (Input.IsActionJustPressed("ui_text_clear_carets_and_selection"))
        {
            if (mouse_captured)
            {
                release_mouse();
            }
            else
            {
                capture_mouse();
            }
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion)
        {
            if (mouse_captured)
            {
                _rotate_camera(motion.Relative);
            }
        }
        // if Input.is_action_just_pressed("jump"): jumping = true
        // if Input.is_action_just_pressed("exit"): get_tree().quit()
    }


    private void capture_mouse()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        mouse_captured = true;
    }

    private void release_mouse()
    {
        Input.MouseMode = Input.MouseModeEnum.Visible;
        mouse_captured = false;
    }

    private void _rotate_camera(Vector2 look_dir)
    {
        float xRad = Mathf.DegToRad(look_dir.X * camera_sens);
        player.RotateObjectLocal(new Vector3(0, -1, 0), xRad);

        float newYAngle = xRotation + look_dir.Y * camera_sens;
        xRotation = Mathf.Clamp(newYAngle, -90f, 90f);


        // RotateObjectLocal(Vector3.Up, yAngles);
        Basis = new Basis(new Vector3(-1, 0, 0), Mathf.DegToRad(xRotation));
        // headTilt.setlocal(new  Vector3(-1, 0, 0), xRad);
    }
}