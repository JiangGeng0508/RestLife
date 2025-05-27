using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : ItemList
{
	public LinkedList<Item> Items = new LinkedList<Item>();
	public void AddItem(Item item)
	{
		Items.AddLast(item);
	}
	public void GenerateButton(Item item)
	{
		
	}
}
