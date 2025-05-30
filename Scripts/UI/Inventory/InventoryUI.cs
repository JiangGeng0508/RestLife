using Godot;
using Godot.NativeInterop;
using System;
using System.Collections.Generic;
//仅控件，实际数据由Inventory处理
public partial class InventoryUI : ItemList
{
	private const int _ButtonSize = 50;
	public RightClickMenu rightClickMenu;
	public static Dictionary<string, int> ItemsInUI = [];//对应物品名与index
	public override void _Ready()
	{
		Global.InventoryUI = this;
		rightClickMenu = GetNode<RightClickMenu>("RightClickMenu");

	}
	public void AddItem(Item item)//添加物品,默认加入第一个位置
	{
		if (item == null) return;
		AddItem($"{item.Name} ({item.Number})", item.Icon);
		Update();
	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		if (mouseButtonIndex == 2)//右键点击
		{
			rightClickMenu.Invoke(GetItemText(index));
		}
	}
	public void Update()//更新背包
	{
		var items = Global.Inventory.GetItems();
		Clear();
		foreach (var item in items)
		{
			ItemsInUI[item.Key] = AddItem($"{item.Value.Name} ({item.Value.Number})", item.Value.Icon);
		}
	}
	public void EquipToSlot(EquipbleItem item, EquipType equipType)
	{

	}
	public void DeleteItem(string name)
	{

	}
}
