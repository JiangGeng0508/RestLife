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
	}
	public static void AddItem(Item item, int number = 1)
	{
		if (Items.ContainsKey(item.Name))
		{
			item.AddNumber(number);
		}
		else
		{
			Items.Add(item.Name, item);
		}
	}
	public static void RemoveItem(Item item, int number = -1)
	{
		if (Items.ContainsKey(item.Name))
		{
			return;
		}
		if (number == -1)
		{
			Items.Remove(item.Name);
		}
		else
		{
			item.AddNumber(-number);
		}
	}
	public static void Equip()
	{
		//TODO
	}
}
