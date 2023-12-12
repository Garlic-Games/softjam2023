using Godot;
using Softjam2023.Scripts.StateMachine;

public partial class HeadRotation : Node3D
{
    
    
    [Export] private float camera_sens = 0.2f;

    private Vector2 look_dir;

    private MainPlayerController player;

    private float xRotation = 0f;

    private bool _gamePaused = true;

    public override void _Ready()
    {
        player = GetParent<MainPlayerController>();
        if (player.GameTime != null) {
            player.GameTime.GamePause += GamePauseStateChange;
        } else {
            _gamePaused = false;
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion)
        {
            if (!_gamePaused)
            {
                _rotate_camera(motion.Relative);
            }
        }
        // if Input.is_action_just_pressed("jump"): jumping = true
        // if Input.is_action_just_pressed("exit"): get_tree().quit()
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

    public void GamePauseStateChange(bool newState) {
        _gamePaused = newState;
    }
}