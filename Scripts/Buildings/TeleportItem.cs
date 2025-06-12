using Godot;
using System;

public partial class TeleportItem : InteractableItem
{
	[Export] public string SceneName { get; set; } = "Tutorial";
	public override void Action()
	{
		GetNode<Sprite2D>("Door/DoorFrontLeft").Hide();
		Global.MainScene.MapManager.Teleport(SceneName);
	}
}
