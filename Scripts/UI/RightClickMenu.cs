using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	int Id = 0;
	public override void _Ready()
	{

	}
	public void MergeVoidFunc(Action action)
	{
		AddItem(action.Method.Name, Id);
		
		Id++;
	}
}
