using Godot;
using Godot.Collections;

public partial class Inventory : Node
{
	public Dictionary<string, Item> Items = [];
	public InventoryUI UI;
	public override void _Ready()
	{
		UI = Global.InventoryUI;
		//TODO: Load

	}
	public void AddItem(Item item, int number = 1)
	{
		if (Items.ContainsKey(item.Name))
		{
			Items[item.Name].AddNumber(number);
		}
		else
		{
			item.Init();
			Items.Add(item.Name, item);
		}
		UI.Update();
	}
	public void RemoveItem(Item item, int number = -1)
	{
		if (!Items.ContainsKey(item.Name))
		{
			return;
		}
		if (number == -1)
		{
			Items.Remove(item.Name);
		}
		else
		{
			Items[item.Name].AddNumber(-number);
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
	public Dictionary<string, Item> GetItems()
	{
		return Items;
	}
}
