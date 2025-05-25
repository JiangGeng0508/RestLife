using Godot;
using System;

public partial class StartGame : Button
{
	public void onPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Main.tscn");
	}
}
