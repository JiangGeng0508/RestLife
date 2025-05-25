using Godot;
using System;

public partial class Chara : CharacterBody2D
{
	bool isMoving = false;
	Vector2 targetPosition = Vector2.Zero;
	float speed = 10f;
	public override void _Ready()
	{
		targetPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isMoving && (targetPosition - Position).Length() > speed)
		{
			Velocity = (targetPosition - Position) / (targetPosition - Position).Length() * speed;
			Velocity = new Vector2(Velocity.X, 0);
			MoveAndCollide(Velocity);
			// MoveAndSlide();
			// MoveAndCollide((targetPosition - Position) / (targetPosition - Position).Length() * speed);
		}
		else
		{
			isMoving = false;
		}

	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.IsPressed())
			{
				GD.Print("Right Mouse Button Pressed");
				if (IsOnFloor() || true)
				{
					isMoving = true;
					targetPosition = GetGlobalMousePosition();
					GD.Print($"from {Position} to {targetPosition}");
				}
			}
		}
	}

}
