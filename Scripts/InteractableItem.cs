using Godot;
using System;

public partial class InteractableItem : Area2D
{
	bool Interactable = false;
	public Area2D CharaReachArea;
	public override void _MouseEnter()
	{
		//外边框发光效果
	}
	public override void _MouseExit()
	{

	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				if (Interactable)
				{
					Action();
				}
			}
		}
	}
	public void onAreaEntered(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			GD.Print("ReachArea entered");
			Interactable = true;
			CharaReachArea = area;
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			Interactable = false;
		}
	}
	public virtual void Action(){}
}
