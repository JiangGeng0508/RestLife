using Godot;
using System;

public partial class Chara : CharacterBody2D
{
	Area2D reachArea;
	TargetNotifer tarPosNotifer;
	InteractableItem interactItem;
	ulong Id = 0;
	bool handable = false;
	bool isMoving = false;
	Vector2 targetPosition = Vector2.Zero;
	float speed = 10f;
	public override void _Ready()
	{
		Id = GetInstanceId();
		reachArea = GetNode<Area2D>("ReachArea");
		tarPosNotifer = GetNode<TargetNotifer>("../TargetNotifier");

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
		if (GetTree().GetNodesInGroup($"ReachedItem{Id}").Count > 0)
		{
			interactItem = GetTree().GetFirstNodeInGroup($"ReachedItem{Id}") as InteractableItem;
			foreach (InteractableItem item in GetTree().GetNodesInGroup($"ReachedItem{Id}"))
			{
				if (interactItem.Position.DistanceTo(Position) > item.Position.DistanceTo(Position))
				{
					interactItem = item;
					handable = true;
				}
			}
		}
		else
		{
			handable = false;
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
					var mousePos = GetGlobalMousePosition();
					tarPosNotifer.Position = new Vector2(mousePos.X, Position.Y);
					tarPosNotifer.ShowTarget();
					isMoving = true;
					targetPosition = mousePos;
				}
			}
		}
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Pressed && keyEvent.Keycode == Key.E && handable)
			{
				interactItem.Action();
			}
		}
	}
	public void onAreaEntered(Area2D area)
	{
		if (area is InteractableItem item)
		{
			item.AddToGroup($"ReachedItem{Id}");
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area.IsInGroup($"ReachedItem{Id}"))
		{
			area.RemoveFromGroup($"ReachedItem{Id}");
		}
	}
}
