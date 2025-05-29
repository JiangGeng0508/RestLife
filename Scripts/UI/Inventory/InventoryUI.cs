using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
public partial class InventoryUI : ItemList
{
	private const int _ButtonSize = 50;
	public RightClickMenu rightClickMenu;
	public static Dictionary<string, Item> ItemGroup = [];
	public override void _Ready()
	{
		rightClickMenu = GetNode<RightClickMenu>("RightClickMenu");

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
	public void AddItem(Item item)//添加物品,默认加入第一个位置
	{
		if (item == null) return;
		item.Init();
		ItemGroup[item.Name] = item;
		AddItem(item.Name);//继承ItemList类的方法
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
		// Clear();
	}
	public void EquipToSlot(EquipbleItem item, EquipType equipType)
	{

	}
	public void DeleteItem(string name)
	{

	}
}
