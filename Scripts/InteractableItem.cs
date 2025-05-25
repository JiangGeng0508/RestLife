using Godot;
using System;

public partial class InteractableItem : Area2D
{
	bool Interactable = false;

	public override void _MouseEnter()
	{
		GD.Print("Mouse Entered");
	}
	public override void _MouseExit()
	{
		GD.Print("Mouse Exited");
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
	public void Action()
	{
		GD.Print("Action");
	}
}
