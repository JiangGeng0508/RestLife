using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : ItemList
{
	private const int _ButtonSize = 50;
	public RightClickMenu rightClickMenu;
	public override void _Ready()
	{
		rightClickMenu = GetNode<RightClickMenu>("RightClickMenu");
		var item = new Item();
		item.Name = "Item1";
		AddItem(item);
	}
	public void AddItem(Item item)
	{
		AddItem(item.Name);//ItemList类的方法

	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		GD.Print("Item clicked: " + GetItemText(index) + " at position: " + position + " with button index: " + mouseButtonIndex);
		if (mouseButtonIndex == 2)//右键点击
		{
			rightClickMenu.Show();
			rightClickMenu.Position = (Vector2I)position;
			rightClickMenu.MergeVoidFunc(TestVoidFunc);
		}
	}
	public void TestVoidFunc()
	{
		GD.Print("TestVoidFunc called");
	}
}
