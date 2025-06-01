using Godot;
using System;
public enum NpcState
{
	Idle,//静止
	WaitingWithQuest,//有任务，等待接取
	Wandering,//漫游
	Forwarding,//有目的地，前往目的地
	Busying,//忙碌
}
public partial class Npc : SelectIntItem
{
	public bool Interactable = true;
	public Random Random = new();
	public Vector2? TargetPosition = null;
	private NpcState _state = NpcState.Idle;
	public Timer Timer;
	//debug
	public ColorRect ColorRect;
	public NpcState State
	{
		get
		{
			return _state;
		}
		set
		{
			ColorRect.Color = new Color(1, 1, 1);//白色
			switch (value)
			{
				case NpcState.Idle:
					Interactable = true;
					break;
				case NpcState.WaitingWithQuest:
					ColorRect.Color = new Color(1, 0, 0);//红色
					Interactable = true;
					break;
				case NpcState.Wandering:
					Interactable = true;
					break;
				case NpcState.Forwarding:
					if (TargetPosition == null)
					{
						State = NpcState.Idle;
						break;
					}
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
		Timer.Connect("timeout", new Callable(this, "OnTimerTimeout"));
		MergeVoidFunc(ChangeState);

		//debug
		ColorRect = GetNode<ColorRect>("ColorRect");
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
				Position += new Vector2(Random.Next(-1, 2), Random.Next(-1, 2));
				break;
			case NpcState.Forwarding:
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
		switch (State)
		{
			case NpcState.Idle:
				Interactable = true;
				Timer.WaitTime = Random.Next(1, 5);
				State = NpcState.Wandering;
				break;
			case NpcState.Wandering:
				Interactable = true;
				Timer.WaitTime = Random.Next(1, 5);
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
		StateSelectionMenu.IdPressed += ChangeStateId;
		StateSelectionMenu.Position = new Vector2I(600, 300);
		StateSelectionMenu.Show();
		StateSelectionMenu.PopupHide += StateSelectionMenu.QueueFree;
	}
	public void ChangeStateId(long Id)
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
				State = NpcState.Forwarding;
				break;
			case 4:
				State = NpcState.Busying;
				break;
		}
	}
}
