using Godot;
using System;

public partial class Menu : Control
{
	public Button Save;

	public override void _Ready()
	{
		Save = GetNode<Button>("Save");
		Save.Connect("pressed", new Callable(this,nameof(SaveGame)));
	}
	public void SaveGame()
	{
		Global.Saver.Call("Save");
	}
}
