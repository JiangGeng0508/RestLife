using Godot;
using System;

public enum CharacterState
{
	Idle,
	Moving,
	Riding,
}
public partial class Character : CharacterBody2D
{
	Area2D reachArea;
	TargetNotifer tarPosNotifer;
	InteractableItem interactItem;
	Label label;//debug
	ulong Id = 0;
	bool handable = false;
	CharacterState state = CharacterState.Idle;
	Vector2 targetPosition = Vector2.Zero;
	float speed = 10f;
	Vector2 _lastPosition = Vector2.Zero;
	public override void _Ready()
	{
		Id = GetInstanceId();
		reachArea = GetNode<Area2D>("ReachArea");
		tarPosNotifer = GetNode<TargetNotifer>("../TargetNotifier");
		label = new Label();//debug
		AddChild(label);//debug
		label.Text = state.ToString();//debug
		label.Show();//debug
		targetPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		label.Text = state.ToString();//debug
		if (IsMoving())
		{
			if ((targetPosition - Position).Length() > speed)
			{
				Velocity = (targetPosition - Position) / (targetPosition - Position).Length() * speed;
				MoveAndCollide(Velocity);
			}
			else
			{
				state = CharacterState.Idle;
			}
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
				if (!IsRiding())
				{
					var mousePos = GetGlobalMousePosition();
					tarPosNotifer.Position = new Vector2(mousePos.X, Position.Y);
					tarPosNotifer.ShowTarget();
					targetPosition = new Vector2(mousePos.X, Position.Y);
					state = CharacterState.Moving;
				}
				else if (IsRiding())
				{
					StopRiding();
				}
			}
		}
		if (@event is InputEventKey keyEvent)
		{
			if (keyEvent.Pressed && keyEvent.Keycode == Key.E && handable)
			{
				interactItem.Action();
			}
			else if (keyEvent.Pressed && keyEvent.Keycode == Key.A)
			{
				targetPosition = new Vector2(Position.X - speed * 2, Position.Y);
				state = CharacterState.Moving;
				
			}
			else if (keyEvent.Pressed && keyEvent.Keycode == Key.D)
			{
				targetPosition = new Vector2(Position.X + speed * 2, Position.Y);
				state = CharacterState.Moving;
			}
		}
	}
	public void Ride(RideableItem chair)
	{
		if (!IsRiding())
		{
			_lastPosition = Position;
			Position = chair.Position + chair.riderOffset;
			state = CharacterState.Riding;
		}
	}
	public void StopRiding()
	{
		Position = _lastPosition;
		state = CharacterState.Idle;
	}
	public bool IsRiding()
	{
		return (state == CharacterState.Riding);
	}
	public bool IsMoving()
	{
		return (state == CharacterState.Moving);
	}
	public void OnAreaEntered(Area2D area)
	{
		if (area is InteractableItem item)
		{
			item.AddToGroup($"ReachedItem{Id}");
		}
	}
	public void OnAreaExited(Area2D area)
	{
		if (area.IsInGroup($"ReachedItem{Id}"))
		{
			area.RemoveFromGroup($"ReachedItem{Id}");
		}
	}
}
