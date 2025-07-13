using Godot;
using System;

public partial class StartMenu : Node2D
{
	[Export]
	PackedScene LoadScene { get; set; } = GD.Load<PackedScene>("res://Save/MainScene.tscn");
	public override void _Ready()
	{
		//清空Player
		Global.Player = null;
	}
	public void StartGamePressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/MainScene.tscn");
	}
	public void LoadPressed()
	{
		GetTree().ChangeSceneToPacked(LoadScene);
	}
	public void QuitPressed()
	{
		GetTree().Quit();
	}
	public void EditorPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/TresEditor/TresEditor.tscn");
	}
}
