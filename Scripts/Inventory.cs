using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : BoxContainer
{
	public LinkedList<Item> Items = new LinkedList<Item>();
	public override void _Ready()
	{
		Visible = false;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Keycode == Key.Tab)
		{
			Visible =!Visible;
		}
	}
	public void AddItem(Item item)
	{
		Items.AddLast(item);
	}
}
