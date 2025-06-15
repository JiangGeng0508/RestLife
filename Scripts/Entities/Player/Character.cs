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
	public AnimatedSprite2D PlayerAnim;
	TargetNotifer tarPosNotifer;
	InteractableItem interactItem;
	RideableItem ridingItem;
	AttributeManager attributes;
	public ulong Id = 0;
	bool isRunning = false;
	bool handable = false;
	private CharacterState _state = CharacterState.Idle;
	public CharacterState State
	{
		get => _state;
		set
		{
			if (_state != value)
			{
				_state = value;
				switch (_state)
				{
					case CharacterState.Idle:
						PlayerAnim.Play("Idle");
						break;
					case CharacterState.Moving:
						PlayerAnim.Play("Walk");
						if (isRunning)
						{
							PlayerAnim.Play("Run");
						}
						break;
					case CharacterState.MovingbyKeyboard:
						PlayerAnim.Play("Walk");
						if (isRunning)
						{
							PlayerAnim.Play("Run");
						}
						break;
					case CharacterState.Riding:
						PlayerAnim.Play("Sit");
						break;
					case CharacterState.Waiting:
						PlayerAnim.Play("Wait");
						break;
				}
			}
		}
	}
	Vector2 targetPosition = Vector2.Zero;
	CharacterState prevState = CharacterState.Idle;

	float Speed = 150f;
	private Vector2 _prevPosition = Vector2.Zero;
	int KeyDirection = 0;

	public override void _Ready()
	{
		Id = GetInstanceId();
		GD.Print($"My Id is {Id}");
		reachArea = GetNode<Area2D>("ReachArea");
		tarPosNotifer = GetNode<TargetNotifer>("../TargetNotifier");
		targetPosition = Position;
		PlayerAnim = GetNode<AnimatedSprite2D>("PlayerAnim");
		PlayerAnim.AnimationFinished += UpdataAnim;
		PlayerAnim.Play("Idle");
		attributes = GetNode<AttributeManager>("../AttributeManager");

		Multiplayer.ConnectedToServer += () =>
		{
			GD.Print($"Connected to server, my Id is {Id}");
			Id = (ulong)GetMultiplayerAuthority();
		};
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (State)
		{
			case CharacterState.Idle:
				attributes.Hunger.Value -= 0.1f * (float)delta;
				break;
			case CharacterState.Moving:
				if ((targetPosition - Position).Length() > 10f)
				{
					Velocity = (targetPosition - Position).Normalized() * Speed * (float)delta;
					if (Velocity.X > 0)
					{
						PlayerAnim.FlipH = false;
					}
					else if (Velocity.X < 0)
					{
						PlayerAnim.FlipH = true;
					}
					if (MoveAndCollide(Velocity) != null)
					{
						State = CharacterState.Idle;
					}
				}
				else
				{
					State = CharacterState.Idle;
				}
				attributes.Energy.Value -= 0.1f * (float)delta;
				attributes.Hunger.Value -= 0.1f * (float)delta;
				if (isRunning)
				{
					attributes.Hunger.Value -= 0.3f * (float)delta;
					attributes.Energy.Value -= 0.2f * (float)delta;
				}
				break;
			case CharacterState.MovingbyKeyboard:
				if (KeyDirection != 0)
				{
					Velocity = new Vector2(KeyDirection, 0) * Speed * (float)delta;
					MoveAndCollide(Velocity);
					if (Velocity.X > 0)
					{
						PlayerAnim.FlipH = false;
					}
					else if (Velocity.X < 0)
					{
						PlayerAnim.FlipH = true;
					}
				}
				else
				{
					State = CharacterState.Idle;
				}

				attributes.Energy.Value -= 0.1f * (float)delta;
				attributes.Hunger.Value -= 0.1f * (float)delta;
				if (isRunning)
				{
					attributes.Hunger.Value -= 0.3f * (float)delta;
					attributes.Energy.Value -= 0.2f * (float)delta;
				}
				break;
			case CharacterState.Riding:
				attributes.Energy.Value += 0.5f * (float)delta;
				attributes.Hunger.Value -= 0.1f * (float)delta;
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
					State = CharacterState.Moving;
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
					AfterAction();
					interactItem.Action();
				}
				if (!IsRiding() && !IsWaiting())
				{
					if (keyEvent.Keycode == Key.A)
					{
						KeyDirection = -1;
						State = CharacterState.MovingbyKeyboard;
					}
					else if (keyEvent.Keycode == Key.D)
					{
						KeyDirection = 1;
						State = CharacterState.MovingbyKeyboard;
					}
				}
				else if (IsRiding() && keyEvent.Keycode == Key.R)
				{
					StopRiding();
				}
			}
			else if (IsMovingbyKeyboard())//按键抬起时处理
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
					State = CharacterState.Idle;
				}
			}
			//跑步
			if (keyEvent.Keycode == Key.Shift)
			{
				if (keyEvent.Pressed)
				{
					if (IsMoving() || IsMovingbyKeyboard())
					{
						isRunning = true;
						Speed = 250f;
						PlayerAnim.Play("Run");
					}
				}
				else
				{
					if (isRunning)
					{
						if (IsMoving() || IsMovingbyKeyboard())
						{
							PlayerAnim.Play("Walk");
						}
						else
						{
							PlayerAnim.Play("Idle");
						}
					}
					isRunning = false;
					Speed = 150f;
				}
			}
		}
	}
	public void Ride(RideableItem chair)
	{
		if (!IsRiding() && !IsWaiting())
		{
			_prevPosition = Position;
			Position = chair.Position + chair.RiderOffset;
			ridingItem = chair;
			State = CharacterState.Riding;
		}
	}
	public void StopRiding()
	{
		Position = _prevPosition;
		KeyDirection = 0;
		ridingItem.rider = null;
		State = CharacterState.Idle;
	}
	public bool IsRiding() => State == CharacterState.Riding;
	public bool IsMoving() => State == CharacterState.Moving;
	public bool IsMovingbyKeyboard() => State == CharacterState.MovingbyKeyboard;
	public bool IsIdle() => State == CharacterState.Idle;
	public bool IsWaiting() => State == CharacterState.Waiting;
	public void AfterAction()
	{
		if (IsMoving() || IsMovingbyKeyboard())
		{
			KeyDirection = 0;
			State = CharacterState.Idle;
		}
		switch (interactItem.GetType().Name)
		{
			case "ItemDrop":
				PlayerAnim.Play("Pickup");
				break;
			case "InfCrate":
			case "SelectIntItem":
				PlayerAnim.Play("Wait");
				break;
			case "Npc":
				PlayerAnim.Play("Talk");
				break;
			case "RideableItem":
				PlayerAnim.Play("Sit");
				break;
			default:
				PlayerAnim.Play("Interact");
				break;
		}
	}
	public void UpdataAnim()
	{
		switch (State)
		{
			default:
				PlayerAnim.Play("Idle");
				break;
		}
	}

	public void OnToggleMenu()
	{
		if (Global.SelectMenu.Visible)
		{
			prevState = (State == CharacterState.Moving) ? CharacterState.Idle : State;
			KeyDirection = 0;
			State = CharacterState.Waiting;
		}
		else
		{
			State = prevState;
		}
	}
}
