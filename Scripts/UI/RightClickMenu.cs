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
	public void MergeVoidFunc(Action action)
	{
		//添加选项
		AddItem(action.Method.Name, Id);
		Id++;
	}
	public void Invoke(int index)//调出时更新
	{
		//获取item
		var item = GetTree().GetNodesInGroup("Item")[index] as Item;
		foreach (var action in item.actions)
		{
			MergeVoidFunc(action);
		}
	}
	public void OnLoseFocus()
	{
		Hide();
		Clear();
	}
}
