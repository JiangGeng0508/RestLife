using Godot;
using System;

public partial class InteractableItem : Area2D
{
	public bool Interactable = false;
	public bool MouseIn = false;
	public Area2D CharaReachArea;
	public Label label;
	public ColorRect sprite;
	public override void _Ready()
	{
		Connect("area_entered", new Callable(this, nameof(onAreaEntered)));
		Connect("area_exited", new Callable(this, nameof(onAreaExited)));
		sprite = GetNode<ColorRect>("ColorRect");

		Init();
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Interactable)
		{
			if (MouseIn)
			{
				sprite.Color = new Color(0, 1, 0, 1);//绿色
			}
			else
			{
				sprite.Color = new Color(255, 0, 0, 1);//红色
			}
		}
		else
		{
			sprite.Color = new Color(1, 1, 1, 1);//白色
		}
	}

	public override void _MouseEnter()
	{
		MouseIn = true;
		// GD.Print($"MouseEnter{Time.GetUnixTimeFromSystem()}");
	}
	public override void _MouseExit()
	{
		MouseIn = false;
		// GD.Print($"MouseExit{Time.GetUnixTimeFromSystem()}");
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (Interactable && MouseIn && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed )
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
			AddToGroup("ReachedItem"+id.ToString());
			Interactable = true;
			CharaReachArea = area;
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			var id = reachArea.GetParent().GetInstanceId();
			RemoveFromGroup("ReachedItem"+id.ToString());
			Interactable = false;
			CharaReachArea = null;
		}
	}
	public virtual void Init() { }
	public virtual void Action() { }
}
