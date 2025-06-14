using Godot;
using System;
public enum NpcState
{
	Idle,//静止
	WaitingWithQuest,//有任务，等待接取
	Wandering,//漫游
	Backwarding,//有目的地，前往目的地
	Busying,//忙碌
}
public enum NpcActivity
{
	Random,//随机行动
	Idle,//静止
}
public partial class Npc : SelectIntItem
{
	[Export]
	public NpcActivity Activity { get; set; } = NpcActivity.Idle;
	public bool Interactable = true;
	Random Random = new();
	float RandomDirection = 0;
	float Speed = 100;
	private NpcState _state = NpcState.Idle;
	public Timer Timer;
	public NpcState State
	{
		get
		{
			return _state;
		}
		set
		{
			DelFunc(PickUpQuest);
			switch (value)
			{
				case NpcState.Idle:
					Interactable = true;
					break;
				case NpcState.WaitingWithQuest:
					MergeVoidFunc(PickUpQuest);
					Interactable = true;
					break;
				case NpcState.Wandering:
					Interactable = true;
					break;
				case NpcState.Backwarding:
					Interactable = false;
					break;
				case NpcState.Busying:
					Interactable = false;
					break;
			}
			_state = value;
		}
	}
	public override void OnBegin()
	{
		Timer = GetNode<Timer>("Timer");
		Timer.Start();
		Timer.WaitTime = 1f;
		Timer.Timeout += OnTimerTimeout;
		MergeVoidFunc(ChangeState);

		if (Activity == NpcActivity.Idle)
		{
			Timer.Stop();
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		switch (_state)
		{
			case NpcState.Idle:
				break;
			case NpcState.WaitingWithQuest:
				break;
			case NpcState.Wandering:
				Position += new Vector2(RandomDirection, 0) * Speed * (float)delta;
				break;
			case NpcState.Backwarding:
				if (Position.DistanceTo(Vector2.Zero) < 10)
				{
					State = NpcState.Idle;
					break;
				}
				Position += new Vector2(-Position.X, 0).Normalized() * Speed * (float)delta;
				break;
			case NpcState.Busying:
				break;
			default:
				break;
		}
	}
	public void OnTimerTimeout()
	{
		UpdataState();
	}
	public void UpdataState()
	{
		RandomDirection = (float)Random.NextDouble() * 2 - 1;
		switch (State)
		{
			case NpcState.Idle:
				Timer.WaitTime = Random.Next(1, 5);
				State = NpcState.Wandering;
				break;
			case NpcState.Wandering:
				Timer.WaitTime = Random.Next(1, 5);
				if (Position.X < -100 || Position.X > 100)
				{
					State = NpcState.Backwarding;
					break;
				}
				State = NpcState.Idle;
				break;
			default:
				break;
		}
	}
	//debug
	public void ChangeState()
	{
		GD.Print("ChangeState");
		var StateSelectionMenu = new PopupMenu();
		AddChild(StateSelectionMenu);
		StateSelectionMenu.AddItem("Idle", 0);
		StateSelectionMenu.AddItem("WaitingWithQuest", 1);
		StateSelectionMenu.AddItem("Wandering", 2);
		StateSelectionMenu.AddItem("Forwarding", 3);
		StateSelectionMenu.AddItem("Busying", 4);
		StateSelectionMenu.IdPressed += ChangeState;
		StateSelectionMenu.Position = new Vector2I(600, 300);
		StateSelectionMenu.Show();
		StateSelectionMenu.MouseExited += StateSelectionMenu.QueueFree;
	}
	public void ChangeState(long Id)
	{
		switch (Id)
		{
			case 0:
				State = NpcState.Idle;
				break;
			case 1:
				State = NpcState.WaitingWithQuest;
				break;
			case 2:
				State = NpcState.Wandering;
				break;
			case 3:
				State = NpcState.Backwarding;
				break;
			case 4:
				State = NpcState.Busying;
				break;
		}
	}
	public void PickUpQuest()
	{
		GD.Print("PickUpQuest");
		State = NpcState.Idle;
	}
}
