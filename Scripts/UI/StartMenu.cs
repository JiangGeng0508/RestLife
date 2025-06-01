using Godot;
using System;

public partial class StartMenu : Node2D
{
	public override void _Ready()
	{
		GetNode<Button>("StartGame").Pressed += StartGamePressed;
		GetNode<Button>("Load").Pressed += LoadPressed;
		GetNode<Button>("Quit").Pressed += QuitPressed;
	}
	public void StartGamePressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
	}
	public void LoadPressed()
	{
		var file = "res://Save/MainScene.tscn";
		if (FileAccess.FileExists(file))
		{
			GetTree().ChangeSceneToFile(file);
		}
	}
	public void QuitPressed()
	{
		GetTree().Quit();
	}
}
