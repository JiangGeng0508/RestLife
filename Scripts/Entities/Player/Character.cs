using Godot;
using System;

public enum CharacterState
{
	Idle,
	Moving,
	MovingbyKeyboard,
	Riding,
	Waiting
}
public partial class Character : CharacterBody2D
{
	Area2D reachArea;
	TargetNotifer tarPosNotifer;
	InteractableItem interactItem;
	RideableItem ridingItem;
	public ulong Id = 0;
	bool handable = false;
	CharacterState state = CharacterState.Idle;
	Vector2 targetPosition = Vector2.Zero;
	CharacterState prevState = CharacterState.Idle;

	float Speed = 500f;
	private Vector2 _prevPosition = Vector2.Zero;
	int KeyDirection = 0;

	// 状态值
	public Attribute Health = new(50f);
	public Attribute Energy = new(100f);
	public Attribute Hunger = new(100f);
	public Attribute Thirst = new(100f);
	// 属性值
	public float Intelligence { get; set; } = 1f;
	public float Strength { get; set; } = 1f;
	public float Charisma { get; set; } = 1f;
	public float Agility { get; set; } = 1f;
	public override void _Ready()
	{
		Global.Player = this;
		Id = GetInstanceId();
		reachArea = GetNode<Area2D>("ReachArea");
		tarPosNotifer = GetNode<TargetNotifer>("../UI/TargetNotifier");
		targetPosition = Position;;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (state)
		{
			case CharacterState.Idle:
				Hunger.Value -= 0.1f * (float)delta;
				Thirst.Value -= 0.1f * (float)delta;
				break;
			case CharacterState.Moving:
				if ((targetPosition - Position).Length() > Speed * 1.5f)
				{
					Velocity = (targetPosition - Position).Normalized() * Speed * (float)delta;
					if (MoveAndCollide(Velocity) != null)
					{
						state = CharacterState.Idle;
					}
				}
				else
				{
					state = CharacterState.Idle;
				}
				Energy.Value -= 0.1f * (float)delta;
				Hunger.Value -= 0.1f * (float)delta;
				Thirst.Value -= 0.1f * (float)delta;
				break;
			case CharacterState.MovingbyKeyboard:
				if (KeyDirection != 0)
				{
					Velocity = new Vector2(KeyDirection, 0) * Speed * (float)delta;
					MoveAndCollide(Velocity);
				}
				else
				{
					state = CharacterState.Idle;
				}
				Energy.Value -= 0.1f * (float)delta;
				Hunger.Value -= 0.1f * (float)delta;
				Thirst.Value -= 0.1f * (float)delta;
				break;
			case CharacterState.Riding:
				Hunger.Value -= 0.1f * (float)delta;
				Thirst.Value -= 0.1f * (float)delta;
				break;
			case CharacterState.Waiting:
				break;
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.IsPressed())
			{
				if (!IsRiding() && !IsWaiting())
				{
					var mousePos = GetGlobalMousePosition();
					tarPosNotifer.Position = new Vector2(mousePos.X, Position.Y);
					tarPosNotifer.ShowTarget();
					targetPosition = new Vector2(mousePos.X, Position.Y);
					state = CharacterState.Moving;
					KeyDirection = 0;
				}
				else if (IsRiding())
				{
					StopRiding();
				}
			}
		}
		if (@event is InputEventKey keyEvent)
		{
			//可互动检测
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

			if (keyEvent.Pressed)
			{
				if (keyEvent.Keycode == Key.E && handable && !IsWaiting())
				{
					interactItem.Action();
					AfterAction();
				}
				if (!IsRiding() && !IsWaiting())
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
				if (keyEvent.Keycode == Key.Tab)
				{
					if (!IsWaiting())
					{
						prevState = (state == CharacterState.Moving) ? CharacterState.Idle : state;
						KeyDirection = 0;
						state = CharacterState.Waiting;
					}
					else
					{
						state = prevState;
					}
				}
			}
			else if (IsMovingbyKeyboard())
			{
				if (keyEvent.Keycode == Key.A && KeyDirection != -1)
				{
					KeyDirection = (KeyDirection == 1) ? 1 : 0;
				}
				else if (keyEvent.Keycode == Key.D && KeyDirection != 1)
				{
					KeyDirection = (KeyDirection == -1) ? -1 : 0;
				}
				else if (keyEvent.Keycode == Key.A || keyEvent.Keycode == Key.D)
				{
					KeyDirection = 0;
					state = CharacterState.Idle;
				}
			}
		}
	}
	public void Ride(RideableItem chair)
	{
		if (!IsRiding() && !IsWaiting())
		{
			_prevPosition = Position;
			Position = chair.Position + chair.riderOffset;
			ridingItem = chair;
			state = CharacterState.Riding;
		}
	}
	public void StopRiding()
	{
		Position = _prevPosition;
		KeyDirection = 0;
		ridingItem.rider = null;
		state = CharacterState.Idle;
	}
	public bool IsRiding() => state == CharacterState.Riding;
	public bool IsMoving() => state == CharacterState.Moving;
	public bool IsMovingbyKeyboard() => state == CharacterState.MovingbyKeyboard;
	public bool IsIdle() => state == CharacterState.Idle;
	public bool IsWaiting() => state == CharacterState.Waiting;
	public void AfterAction()
	{
		if (IsMoving() || IsMovingbyKeyboard())
		{
			KeyDirection = 0;
			state = CharacterState.Idle;
		}
	}
}
