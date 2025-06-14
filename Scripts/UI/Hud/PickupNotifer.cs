using Godot;
using System;

public partial class PickupNotifer : VBoxContainer
{
	public override void _Ready()
	{
		Global.Player.Inventory.ItemAdded += Show;
	}
	public void Show(Item item, int number)
	{
		var label = new Label
		{
			Text = item.Name + " (" + number + ")",
			HorizontalAlignment = HorizontalAlignment.Center
		};
		AddChild(label);
		GetTree().CreateTimer(1f).Timeout += () =>
		{
			label.QueueFree();
		};
	}
}
