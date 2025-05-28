using Godot;
using System;

public partial class RightClickMenu : PopupMenu
{
	public override void _Ready()
	{

	}
	public void MergeVoidFunc(Action action)
	{
		AddItem("Merge");
	}
}
