using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	[Export]
	public Item Item;
	public int Number;
	public Sprite2D Icon;
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
		Icon = new Sprite2D();
		AddChild(Icon);
		Icon.Texture = item.Icon;
		Number = item.Number;
	}
}
