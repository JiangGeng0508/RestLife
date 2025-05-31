using Godot;
using System;

public partial class StartMenu : Node2D
{
	Button StartGame;
	Button Load;

	public override void _Ready()
	{
		StartGame = GetNode<Button>("StartGame");
		Load = GetNode<Button>("Load");
		StartGame.Connect("pressed", new Callable(this,nameof(StartGamePressed)));
		Load.Connect("pressed", new Callable(this,nameof(LoadPressed)));
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
}
