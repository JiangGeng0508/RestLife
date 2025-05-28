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
		var item2 = new EquipbleItem();
		item2.Name = "EquipbleItem1";
		item2.equipType = EquipType.MainHand;
		AddItem(item2);
	}
	public void AddItem(Item item)//添加物品,默认加入第一个位置
	{
		AddChild(item);
		AddItem(item.Name);//继承ItemList类的方法
		item.AddToGroup("Inventory");
	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		if (mouseButtonIndex == 2)//右键点击
		{
			rightClickMenu.Invoke(index);
			rightClickMenu.Show();
		}
	}
	public void InventoryUpdate()//更新背包
	{
		Clear();
		foreach (var item in GetTree().GetNodesInGroup("Inventory"))
		{
			AddItem(item as Item);
		}
	}
	public void DeleteItem(int index)
	{
		RemoveItem(index);
	}
}
