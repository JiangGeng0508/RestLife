using Godot;
using System;
using System.Collections.Generic;

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
	public Attributes Attribute;
	public ulong Id = 0;
	bool handable = false;
	CharacterState state = CharacterState.Idle;
	Vector2 targetPosition = Vector2.Zero;
	CharacterState prevState = CharacterState.Idle;

	float speed = 10f;
	private Vector2 _prevPosition = Vector2.Zero;
	int KeyDirection = 0;

	// 状态值
	private static readonly float MaxHealth = 100f;
	private float _health = 100f;
	public float Health
	{
		get
		{
			return _health;
		}
		set
		{
			if (value < 0)
			{
				_health = 0;
			}
			else if (value > MaxHealth)
			{
				_health = MaxHealth;
			}
			else
			{
				_health = value;
			}
		}
	}
	private static readonly float MaxEnergy = 100f;
	private float _energy = 100f;
	public float Energy
	{
		get
		{
			return _energy;
		}
		set
		{
			if (value < 0)
			{
				_energy = 0;
			}
			else if (value > MaxEnergy)
			{
				_energy = MaxEnergy;
			}
			else
			{
				_energy = value;
			}
		}
	}
	private static readonly float MaxHunger = 100f;
	private float _hunger = 100f;
	public float Hunger
	{
		get
		{
			return _hunger;
		}
		set
		{
			if (value < 0)
			{
				_hunger = 0;
			}
			else if (value > MaxHunger)
			{
				_hunger = MaxHunger;
			}
			else
			{
				_hunger = value;
			}
		}
	}
	private static readonly float MaxThirst = 100f;
	private float _thirst = MaxThirst;
	public float Thirst
	{
		get
		{
			return _thirst;
		}
		set
		{
			if (value < 0)
			{
				_thirst = 0;
			}
			else if (value > MaxThirst)
			{
				_thirst = MaxThirst;
			}
			else
			{
				_thirst = value;
			}
		}
	}

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
		Attribute = GetNode<Attributes>("Attributes");
		tarPosNotifer = GetNode<TargetNotifer>("../UI/TargetNotifier");
		targetPosition = Position;;
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (state)
		{
			case CharacterState.Idle:
				Hunger -= 0.001f * (float)delta;
				Thirst -= 0.001f * (float)delta;
				break;
			case CharacterState.Moving:
				if ((targetPosition - Position).Length() > speed * 1.5f)
				{
					Velocity = (targetPosition - Position).Normalized() * speed;
					if (MoveAndCollide(Velocity) != null)
					{
						state = CharacterState.Idle;
					}
				}
				else
				{
					state = CharacterState.Idle;
				}
				Energy -= 0.01f * (float)delta;
				Hunger -= 0.001f * (float)delta;
				Thirst -= 0.001f * (float)delta;
				break;
			case CharacterState.MovingbyKeyboard:
				if (KeyDirection != 0)
				{
					Velocity = new Vector2(KeyDirection, 0) * speed;
					MoveAndCollide(Velocity);
				}
				else
				{
					state = CharacterState.Idle;
				}
				Energy -= 0.01f * (float)delta;
				Hunger -= 0.001f * (float)delta;
				Thirst -= 0.001f * (float)delta;
				break;
			case CharacterState.Riding:
				Hunger -= 0.001f * (float)delta;
				Thirst -= 0.001f * (float)delta;
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
					state = CharacterState.Idle;
					interactItem.Action();
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
			state = CharacterState.Riding;
		}
	}
	public void StopRiding()
	{
		Position = _prevPosition;
		KeyDirection = 0;
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
