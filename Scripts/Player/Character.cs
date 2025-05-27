using Godot;
using System;

public enum CharacterState
{
	Idle,
	Moving,
	MovingbyKeyboard,
	Riding,
}
public partial class Character : CharacterBody2D
{
	Area2D reachArea;
	TargetNotifer tarPosNotifer;
	InteractableItem interactItem;
	Label label;//debug
	public ulong Id = 0;
	bool handable = false;
	CharacterState state = CharacterState.Idle;
	Vector2 targetPosition = Vector2.Zero;

	float speed = 200f;
	private Vector2 _prevPosition = Vector2.Zero;
	int KeyDirection = 0;
  
	public override void _Ready()
	{
		Id = GetInstanceId();
		reachArea = GetNode<Area2D>("ReachArea");
		tarPosNotifer = GetNode<TargetNotifer>("../UI/TargetNotifier");
		label = new Label();//debug
		AddChild(label);//debug
		label.Show();//debug
		targetPosition = Position;
	}

	public override void _PhysicsProcess(double delta)
	{
		//debug
		label.Text = $"{handable} {GetTree().GetNodesInGroup($"ReachedItem{Id}").Count}";

		// if (IsMoving())
		// {
		// 	if ((targetPosition - Position).Length() > speed)
		// 	{
		// 		Velocity = (targetPosition - Position) / (targetPosition - Position).Length() * speed;
		// 		MoveAndCollide(Velocity);
		// 	}
		// 	else
		// 	{
		// 		state = CharacterState.Idle;
		// 	}
		// }
		switch (state)
		{
			case CharacterState.Idle:
				break;
			case CharacterState.Moving:
				if ((targetPosition - Position).Length() > 1f)
				{
					Velocity = (targetPosition - Position).Normalized() * speed;
					MoveAndSlide();
				}
				else
				{
          			tarPosNotifer.HideTarget();
					state = CharacterState.Idle;
				}
				break;
			case CharacterState.MovingbyKeyboard:
				if (KeyDirection != 0)
				{
					Velocity =  new Vector2(KeyDirection, 0) * speed;
					MoveAndSlide();
				}
				else
				{
					state = CharacterState.Idle;
				}
				break;
			case CharacterState.Riding:
				break;
		}

		if (GetTree().GetNodesInGroup($"ReachedItem{Id}").Count > 0)
		{
			handable = true;
			interactItem = GetTree().GetFirstNodeInGroup($"ReachedItem{Id}") as InteractableItem;
			foreach (InteractableItem item in GetTree().GetNodesInGroup($"ReachedItem{Id}"))
			{
				if (interactItem.Position.DistanceTo(Position) > item.Position.DistanceTo(Position))
				{
					interactItem = item;
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
			if (keyEvent.Pressed)
			{
				if (keyEvent.Keycode == Key.E && handable)
				{
					interactItem.Action();
				}
				if (!IsRiding())
				{
					if (keyEvent.Keycode == Key.A)
					{
						KeyDirection = -1;
						state = CharacterState.MovingbyKeyboard;
					}
					else if (keyEvent.Keycode == Key.D)
					{
						KeyDirection = 1;
						state = CharacterState.MovingbyKeyboard;
					}
				}
				else if (IsRiding() && keyEvent.Keycode == Key.R)
				{
					StopRiding();
				}
			}
			else
			{
				KeyDirection = 0;
				state = CharacterState.Idle;
			}
		}
	}
	public void Ride(RideableItem chair)
	{
		if (!IsRiding())
		{
			_prevPosition = Position;
			Position = chair.Position + chair.riderOffset;
			state = CharacterState.Riding;
		}
	}
	public void StopRiding()
	{
		Position = _prevPosition;
		state = CharacterState.Idle;
	}
	public bool IsRiding()
	{
		return state == CharacterState.Riding;
	}
	public bool IsMoving()
	{
		return state == CharacterState.Moving;
	}
	public bool IsMovingbyKeyboard()
	{
		return state == CharacterState.MovingbyKeyboard;
	}
	public bool IsIdle()
	{
		return state == CharacterState.Idle;
	}
	// public void ChangeState(CharacterState newState)
	// {
	// 	state = newState;
	// }
	// public void ChangeState(string newState)
	// {
	// 	state = (CharacterState)Enum.Parse(typeof(CharacterState), newState);
	// }
	public void AfterAction()
	{
		if (IsMoving())
		{
			state = CharacterState.Idle;
		}
	}
	public void OnAreaEntered(Area2D area)
	{
		if (area is InteractableItem item)
		{
		}
	}
	public void OnAreaExited(Area2D area)
	{
		if (area.IsInGroup($"ReachedItem{Id}"))
		{
		}
	}
}
