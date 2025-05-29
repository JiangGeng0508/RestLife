using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	int Id = 0;
	public Item ActionItem;//当前选中的item
	public Action[] BindActions;
	public override void _Ready()
	{
		BindActions = new Action[9];
		//从背包中读取items
	}
	public void Invoke(string item)//调出时更新
	{
		//获取item
		ActionItem = Inventory.Items[item];
		foreach (var action in ActionItem.actions)
		{
			if (action is not null)
			{
				AddVoidSelection(action);
			}
		}
		Show();
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
		BindActions[id]?.Invoke();
	}
}
