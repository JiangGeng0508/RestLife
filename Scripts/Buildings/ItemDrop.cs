using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	[Export]
	public Item Item;
	public int Number;
	public Sprite2D Icon;
	public override void Init()
	{
		Icon = GetNode<Sprite2D>("Icon");
		if (Item != null)
		{
			Icon.Texture = Item.Icon;
			Name = Item.Name;
			Number = Item.Number;
		}
	}
	public override void Action()
	{
		GD.Print("Pick up " + Item.Name);
		Global.Player.Inventory.Call("AddItem", Item, Number);
		QueueFree();
	}
	public ItemDrop() {}
}
