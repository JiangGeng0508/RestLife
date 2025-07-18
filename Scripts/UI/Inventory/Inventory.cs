using Godot;
using Godot.Collections;

public partial class Inventory : Node
{
	[Signal]
	public delegate void ItemAddedEventHandler(Item item, int number);
	public Dictionary<Item, int> Items = [];
	public InventoryUI UI;
	public NeoInventory NeoInventory;
	public override void _Ready()
	{
		UI = Global.InventoryUI;
		NeoInventory = Global.NeoInventory;
		//TODO: Load

	}
	public void AddItem(Item item, int number = 1)
	{
		if (Items.TryGetValue(item, out int value))
		{
			value += number;
		}
		else
		{
			item.Init();
			Items.Add(item, number);
		}
		EmitSignal(nameof(ItemAdded), item, number);
		UI.Update();
	}
	public void RemoveItem(Item item, int number = -1)
	{
		if (Items.TryGetValue(item, out int value))
		{
				if (number <= 0 || value <= number)
			{
				Items.Remove(item);
			}
			else
			{
				Items[item] -= number;
			}
		}	
		UI.Update();
	}
	public void Drop(Item item)
	{
		var drop = GD.Load<PackedScene>("res://Scenes/Buildings/ItemDrop.tscn").Instantiate<ItemDrop>();
		drop.Item = item;
		drop.Position = Global.Player.Character.GetGlobalPosition();
		AddChild(drop);
		GD.Print(drop.GetParent().Name);
		RemoveItem(item);
	}
	public Dictionary<Item, int> GetItems()
	{
		return Items;
	}
}
