using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	Item Item;
	int Number;
	public override void Action()
	{
		Global.Player.Inventory.Call("AddItem", Item, Number);
		QueueFree();
	}
	public ItemDrop(Item item)
	{
		Item = item;
		GetNode<Sprite2D>("Icon").Texture = item.Icon;
		Number = item.Number;
	}
}
