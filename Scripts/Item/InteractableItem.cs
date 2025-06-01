using Godot;
using System;

public partial class InteractableItem : Area2D
{
	public bool IsPlayerClosed = false;
	public bool MouseIn = false;
	public Area2D CharaReachArea;
	public Label label;
	public ColorRect ColorRect;
	public override void _Ready()
	{
		AreaEntered += onAreaEntered;
		AreaExited += onAreaExited;
		ColorRect = GetNode<ColorRect>("ColorRect");

		Init();
	}
	public override void _PhysicsProcess(double delta)
	{
		if (IsPlayerClosed)
		{
			if (MouseIn)
			{
				ColorRect.Color = new Color(0, 1, 0, 1);//绿色
			}
			else
			{
				ColorRect.Color = new Color(255, 0, 0, 1);//红色
			}
		}
		else
		{
			ColorRect.Color = new Color(1, 1, 1, 1);//白色
		}
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (IsPlayerClosed && MouseIn && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed )
			{
				Action();
				CharaReachArea.GetParent().Call("AfterAction");
			}
		}
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
	public virtual void Init() { }
	public virtual void Action() { }
}
