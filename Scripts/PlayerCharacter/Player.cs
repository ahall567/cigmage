using Godot;
using CigMage.Extensions;

namespace CigMage.PlayerCharacter;

public partial class Player : CharacterBody3D
{
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private float _acceleration = 0.5f;

	private PlayerHead _head;
	
	public override void _Ready()
	{
		_head = this.GetChildByType<PlayerHead>();
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	public override void _Input(InputEvent @event)
	{
		// Show the mouse when escape is pressed.
		if (@event is InputEventKey k && k.Keycode == Key.Escape)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector("move_left", "move_right", "move_forward", "move_backwards");
		// Re-project the direction to the head's transform. This makes "forward" wherever the camera is looking.
		Vector3 direction = (_head.GlobalTransform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		if (direction != Vector3.Zero)
		{
			velocity.X = Mathf.MoveToward(Velocity.X, direction.X * Speed, _acceleration);
			velocity.Z = Mathf.MoveToward(Velocity.Z, direction.Z * Speed, _acceleration);
		}
		else
		{
			// Use MoveToward to slow down instead of immediately stopping
			velocity.X = Mathf.MoveToward(Velocity.X, 0, _acceleration);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, _acceleration);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
