using Godot;
using System;

public partial class Menu : Control
{

	public override void _Ready()
	{
		GetNode<Button>("Save").Pressed += () =>
		{
			Saver.Save();
		};
		GetNode<Button>("Load").Pressed += () =>
		{
			GetTree().ChangeSceneToFile("res://Save/MainScene.tscn");
		};
		GetNode<Button>("Esc").Pressed += () =>
		{
			GetTree().ChangeSceneToFile("res://Scenes/UI/StartMenu.tscn");
		};
	}
}
