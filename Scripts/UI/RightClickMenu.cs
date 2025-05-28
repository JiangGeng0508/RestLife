using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	public override void _Ready()
	{

	}
	public void MergeVoidFunc(Action action)
	{
		var button = new Button();
		button.Text = action.Method.Name;
		button.Connect("pressed", new Callable(this, action.Method.Name));
	}
}
