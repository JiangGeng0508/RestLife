using Godot;
using Godot.Collections;
using System;

public partial class Inventory : Node
{
	public static Dictionary<string, Item> Items = [];
	public static InventoryUI UI;
	public override void _Ready()
	{
		Global.Inventory = this;
		UI = Global.InventoryUI;
		//TODO: Load

	}
	public static void AddItem(Item item, int number = 1)
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
	public static void RemoveItem(Item item, int number = -1)
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
	public Dictionary<string, Item> GetItems()
	{
		return Items;
	}
	public static void Equip()
	{
		//TODO
	}
}
