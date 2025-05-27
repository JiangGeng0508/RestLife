using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : ItemList
{
	private const int _ButtonSize = 50;
	public override void _Ready()
	{

	}
	public void AddItem(Item item)
	{
		AddItem(item.Name);
	}
}
