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
	public void AddItem(Item item)//添加物品,默认加入第一个位置
	{
		AddItem(item.Name);//继承ItemList类的方法
		item.AddToGroup("Inventory");
		GD.Print("Item added: " + item.Name + "\n Index: " + GetTree().GetNodesInGroup("Inventory").IndexOf(item));
	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		GD.Print("Item clicked: " + GetItemText(index) + " at position: " + position + " with button index: " + mouseButtonIndex);
		if (mouseButtonIndex == 2)//右键点击
		{
			rightClickMenu.Invoke(index);
			rightClickMenu.Show();
		}
	}
	public void TestVoidFunc()
	{
		GD.Print("TestVoidFunc called");
	}
}
