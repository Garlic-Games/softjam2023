using Godot;
using System;

public partial class MainPlayerController : CharacterBody3D
{

	public Node3D rifleContainer; 
	public Rifle rifleMesh;
	public Node3D aimPoint;
	public Camera3D camera;
	
	public const float Speed = 5.0f;
	public const float SprintSpeed = 7.0f;
	public const float JumpVelocity = 4.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();

	public override void _Ready()
	{
		aimPoint = GetNode<Node3D>("HeadTilt/AimPoint");
		rifleContainer = GetNode<Node3D>("Visuals/rifle");
		camera = GetNode<Camera3D>("HeadTilt/Camera3D");
		rifleMesh = rifleContainer.GetChild<Rifle>(0);
	}

	public override void _PhysicsProcess(double delta) {
		float speed = Speed;
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y -= gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()) {
			velocity.Y = JumpVelocity;
		}

		if (Input.IsActionPressed("player_sprint")) {
			speed = SprintSpeed;
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector(
			"player_movement_left", 
			"player_movement_right", 
			"player_movement_forward", 
			"player_movement_backward");
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * speed;
			velocity.Z = direction.Z * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
