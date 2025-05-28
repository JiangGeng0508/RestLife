using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : ItemList
{
	private const int _ButtonSize = 50;
	public RightClickMenu rightClickMenu;
	public override void _Ready()
	{
		var item = new Item();
		item.Name = "Item1";
		AddItem(item);
	}
	public void AddItem(Item item)
	{
		AddItem(item.Name);
	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		GD.Print("Item clicked: " + GetItemText(index));
	}
}
