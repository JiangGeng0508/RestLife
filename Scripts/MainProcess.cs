using Godot;
using System;
using System.Linq;

public partial class MainProcess : Node2D
{
	public Node2D CurrentScene = new();
	public override void _Ready()
	{
		Global.MainProcess = this;
		ChangeScene("res://Scenes/UI/StartMenu.tscn");
	}
	public void Pause()
	{
		GetTree().Paused = !GetTree().Paused;
	}
	public void ChangeScene(string scene)
	{
		CurrentScene?.Free();
		CurrentScene = ResourceLoader.Load<PackedScene>(scene).Instantiate<Node2D>();
		AddChild(CurrentScene);
	}
	// public void ChangeScene(PackedScene scene)
	// {
	// 	GetTree().ChangeSceneToPacked(scene);
	// }
}
