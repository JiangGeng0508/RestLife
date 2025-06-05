using Godot;
using Godot.Collections;
using System;
//仅控件，实际数据由Inventory处理
public partial class InventoryUI : SplitContainer
{
	private const int _ButtonSize = 50;
	public RightClickMenu rightClickMenu;
	public ItemList InventoryList;
	public ItemList NumberList;
	public static Dictionary<string, int> ItemsInUI = [];//对应物品名与index
	public override void _Ready()
	{
		Global.InventoryUI = this;
		rightClickMenu = GetNode<RightClickMenu>("RightClickMenu");
		InventoryList = GetNode<ItemList>("InventoryList");
		NumberList = GetNode<ItemList>("NumberList");
	}
	public void AddItem(Item item)//添加物品,默认加入第一个位置
	{
		if (item == null) return;
		InventoryList.AddItem(item.Name, item.Icon);
		NumberList.AddItem(item.Number.ToString());
		Update();
	}
	public void OnItemClicked(int index, Vector2 position, int mouseButtonIndex)
	{
		if (mouseButtonIndex == 2)//右键点击
		{
			rightClickMenu.Invoke(InventoryList.GetItemText(index), position);
		}
	}
	public void Update()//更新背包显示
	{
		var items = Global.Inventory.GetItems();
		InventoryList.Clear();
		NumberList.Clear();
		foreach (var item in items)
		{
			ItemsInUI[item.Key] = InventoryList.AddItem(item.Value.Name, item.Value.Icon);
			NumberList.AddItem(item.Value.Number.ToString());
		}
	}
	public void DeleteItem(string name)
	{

	}
}
