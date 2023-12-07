using Godot;
using System;

public partial class HeadRotation : Node3D
{

    [ExportCategory("Player")] 
    [Export] 
    private float speed = 10f;

    [Export] 
    private float acceleration = 100f;

    [Export] 
    private float jump_height = 1f;
    
    [Export] 
    private float camera_sens = 1f;
        
    private Vector2 look_dir;
    private bool mouse_captured;
    
    public override void _Ready()
    {
        capture_mouse();
    }


    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion motion)
        {
            look_dir = motion.Relative * 0.001f;
            
        }
        if (mouse_captured)
        {
            _rotate_camera();
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

    private void _rotate_camera(float sens_mod = 1.0f)
    {
        RotateY(Rotation.Y - look_dir.X * camera_sens * sens_mod);
        RotateX(Math.Clamp(Rotation.X - look_dir.Y * camera_sens * sens_mod, -1.5f, 1.5f));
    }
    public void _handle_joypad_camera_rotation(float delta, float sens_mod = 1.0f) {
        // var joypad_dir:
        // Vector2 lookInput = Input.GetVector("look_left", "look_right", "look_up", "look_down");
        // if lookInput.() > 0:
        // look_dir += joypad_dir * delta
        // _rotate_camera(sens_mod)
        // look_dir = Vector2.ZERO
    }

    public override void _PhysicsProcess(double delta)
    {
        if (mouse_captured){
            _handle_joypad_camera_rotation((float) delta);
        }
    }
}