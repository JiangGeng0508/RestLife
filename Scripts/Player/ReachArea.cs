using Godot;
using System;

public partial class ReachArea : Area2D
{
	public void onAreaEntered(Area2D area)
	{
		if (area is InteractableItem)
		{
			((InteractableItem)area).Interactable = true;
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is InteractableItem)
		{
			((InteractableItem)area).Interactable = false;
		}
	}
}
