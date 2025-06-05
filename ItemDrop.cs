using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	Item item;
	public override void Action()
	{
		Global.Player.Inventory.Call("AddItem", item, 1);
		QueueFree();
	}
}
