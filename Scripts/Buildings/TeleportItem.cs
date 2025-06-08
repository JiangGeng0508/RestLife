using Godot;
using System;

public partial class TeleportItem : InteractableItem
{
	[Export] public string SceneName { get; set; } = "Tutorial";
	public override void Action()
	{
		Global.MainScene.MapManager.Teleport(SceneName);
	}
}
