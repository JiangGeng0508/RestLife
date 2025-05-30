using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Dictionary<string, Item> Items = [];
	public static InventoryUI UI;
	public override void _Ready()
	{
		Global.Inventory = this;
		UI = Global.InventoryUI;
		// Test Items
		var item = new Item();
		item.Name = "Item1";
		AddItem(item);
		var item2 = new EquipbleItem();
		item2.Name = "EquipbleItem1";
		item2.equipType = EquipType.MainHand;
		AddItem(item2);
		var item3 = new Food();
		item3.Name = "Food1";
		AddItem(item3);
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
