using Godot;
using System;

public partial class ReachArea : Area2D
{
	Character _character;
	ulong Id = 0;
	public override void _Ready()
	{
		_character = GetParent<Character>();
		Id = _character.Id;
	}
	public void onAreaEntered(Area2D area)
	{
		if (area is InteractableItem)
		{
			area.AddToGroup($"ReachedItem{Id}");
			GD.Print($"Character {Id} entered interactable area {area.Name}");
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is InteractableItem)
		{
			area.RemoveFromGroup($"ReachedItem{Id}");
		}
	}
}
