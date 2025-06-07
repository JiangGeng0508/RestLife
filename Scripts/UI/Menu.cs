using Godot;
using System;

public partial class Menu : Control
{

	public override void _Ready()
	{
		GetNode<Button>("Save").Pressed += OnSaveButton;
		GetNode<Button>("SaveScene").Pressed += OnSaveSceneButton;
		GetNode<Button>("Esc").Pressed += OnEscButton;
	}
	public void OnSaveButton()
	{
		Global.Saver.Call("Save");
	}
	public void OnSaveSceneButton()
	{
		Global.Saver.Call("SaveScene", GetNode("/root/MainScene"));
	}
	public void OnEscButton()
	{
		GetTree().ChangeSceneToFile("res://Scenes/UI/StartMenu.tscn");
	}
}
