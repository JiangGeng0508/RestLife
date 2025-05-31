using Godot;
using System;

public partial class Menu : Control
{
	public Button Save;
	public Button SaveScene;

	public override void _Ready()
	{
		Save = GetNode<Button>("Save");
		Save.Connect("pressed", new Callable(this, nameof(OnSaveButton)));
		SaveScene = GetNode<Button>("SaveScene");
		SaveScene.Connect("pressed", new Callable(this, nameof(OnSaveSceneButton)));
	}
	public void OnSaveButton()
	{
		Global.Saver.Call("Save");
	}
	public void OnSaveSceneButton()
	{
		Global.Saver.Call("SaveScene", GetNode("/root/MainScene"));
	}
}
