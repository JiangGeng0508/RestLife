using Godot;
using System;

public partial class TeleportItem : InteractableItem
{
	[Export] public string ScenePath { get; set; } = "res://Scenes/Scene/Tutorial.tscn";
	public override void Action()
	{
		GetTree().ChangeSceneToFile(ScenePath);
	}
}
