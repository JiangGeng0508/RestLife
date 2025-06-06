using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	[Export]
	public Item Item;
	int Number;
	Sprite2D Icon;
	public override void Init()
	{
		Icon = GetNode<Sprite2D>("Icon");
	}
	public override void Action()
	{
		GD.Print("Pick up " + Item.Name);
		Global.Player.Inventory.Call("AddItem", Item, Number);
		QueueFree();
	}
	public ItemDrop() {}
	public ItemDrop(Item item)
	{
		Item = item;
		Icon.Texture = item.Icon;
		Number = item.Number;
	}
}
