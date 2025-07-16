using Godot;
using System;

public partial class InteractableItem : Area2D
{
	[Signal]
	public delegate void OnClickEventHandler();
	[Signal]
	public delegate void OnActionEventHandler();
	public bool IsPlayerClosed = false;
	public bool MouseIn = false;
	public Area2D CharaReachArea;
	public Label label;
	public override void _Ready()
	{
		AreaEntered += onAreaEntered;
		AreaExited += onAreaExited;
		Init();
	}
	public void onAreaEntered(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			var id = reachArea.GetParent().GetInstanceId();
			AddToGroup("ReachedItem" + id.ToString());
			IsPlayerClosed = true;
			CharaReachArea = area;
		}
		else if (area is Cursur cursur)
		{
			MouseIn = true;
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			var id = reachArea.GetParent().GetInstanceId();
			RemoveFromGroup("ReachedItem" + id.ToString());
			IsPlayerClosed = false;
			CharaReachArea = null;
		}
		else if (area is Cursur cursur)
		{
			MouseIn = false;
		}
	}
	public void Click()
	{
		Action();
		EmitSignal(nameof(OnClick));
	}
	public virtual void Init() { }
	public virtual void Action()
	{
		EmitSignal(nameof(OnAction));
	}
}
