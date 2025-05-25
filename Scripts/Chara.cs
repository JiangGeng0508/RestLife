using Godot;
using System;

public partial class Chara : CharacterBody2D
{
	Area2D interactArea;

	bool isMoving = false;
	Vector2 targetPosition = Vector2.Zero;
	float speed = 10f;
	public override void _Ready()
	{
		interactArea = GetNode<Area2D>("InteractArea");

		targetPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (isMoving && (targetPosition - Position).Length() > speed)
		{
			Velocity = (targetPosition - Position) / (targetPosition - Position).Length() * speed;
			Velocity = new Vector2(Velocity.X, 0);
			MoveAndCollide(Velocity);
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
				if (IsOnFloor() || true)
				{
					isMoving = true;
					targetPosition = GetGlobalMousePosition();
				}
			}
		}
	}

}
