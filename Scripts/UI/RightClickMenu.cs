using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	int Id = 0;
	public Item[] Items;
	public Action[] BindActions;
	public override void _Ready()
	{
		BindActions = new Action[9];
		//从背包中读取items
	}
	public void Invoke(int index)//调出时更新
	{
		//获取item
		var item = GetTree().GetNodesInGroup("Inventory")[index] as Item;
		GD.Print(item.Name);
		foreach (var action in item.actions)
		{
			if (action is null) continue;
			AddVoidSelection(action);
		}
	}
	public void AddVoidSelection(Action action)
	{
		//添加选项
		AddItem(action.Method.Name, Id);
		BindActions[Id] = action;
		//绑定动作
		Id++;
	}
	public void OnLoseFocus()
	{
		Hide();
		Clear();
		Id = 0;
	}
	public void OnIdPressed(int id)
	{
		if (BindActions[id] is not null)
		{
			GD.Print($"Invoke action {BindActions[id].Method.Name}");
			BindActions[id].Invoke();
		}
	}
}
