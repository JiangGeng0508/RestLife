using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	int Id = 0;
	private Vector2I ParentOffset = new(0, 0);
	public Action[] BindActions;
	public override void _Ready()
	{
		BindActions = new Action[9];
		ParentOffset = (Vector2I)GetParent<Control>().GlobalPosition;
		Hide();
	}
	public void Invoke(string item,Vector2 position)//调出时更新
	{
		foreach (var action in Inventory.Items[item].actions)
		{
			if (action is not null)
			{
				AddVoidSelection(action);
			}
		}
		Position = (Vector2I)position + ParentOffset;
		Show();
	}
	public void Invoke(Vector2 position)//非物品node调用
	{
		//先加入选项AddVoidSelection()
		Position = (Vector2I)position + ParentOffset;
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
	public void OnLoseFocus()//失焦归零
	{
		Hide();
		Clear();
		BindActions.Initialize();
		Id = 0;
	}
	public void OnIdPressed(int id)
	{
		BindActions[id]?.Invoke();
	}
}
