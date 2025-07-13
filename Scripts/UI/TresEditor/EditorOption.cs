using Godot;
using System;

public partial class EditorOption : Control
{
	public void BackToMenu()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/StartMenu.tscn");
	}
}
