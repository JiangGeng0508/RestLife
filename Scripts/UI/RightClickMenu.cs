using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	int Id = 0;
	public Item[] Items;
	public override void _Ready()
	{
		//从背包中读取items
	}
	public void AddVoidSelection(Action action)
	{
		//添加选项
		AddItem($"func{Id}", Id);
		//绑定动作
		Id++;
	}
	public void Invoke(int index)//调出时更新
	{
		//获取item
		var item = GetTree().GetNodesInGroup("Inventory")[index] as Item;
		GD.Print(item.Name);
		foreach (var action in item.actions)
		{
			AddVoidSelection(action);
		}
	}
	public void OnLoseFocus()
	{
		Hide();
		Clear();
		Id = 0;
	}
}
