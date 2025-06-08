using Godot;
using System;

public partial class MapManager : Node
{
	Vector2 lastPosition = Vector2.Zero;
	public void Teleport(string mapName)
	{
		ChangeMap(mapName);
		lastPosition = Global.Player.Character.Position;
		GD.Print(lastPosition);
	}
	public void ChangeMap(string mapName)
	{
		if (GetChildCount() == 0)
		{
			AddChild(GD.Load<PackedScene>("res://Scenes/Scene/TestMap1.tscn").Instantiate());
		}
		var preChildren = GetChildren();
		foreach (var child in preChildren)
		{
			RemoveChild(child);
		}
		AddChild(GD.Load<PackedScene>("res://Scenes/Scene/" + mapName + ".tscn").Instantiate());
		if (lastPosition == Vector2.Zero)
		{
			Global.Player.Character.Position = lastPosition;
			lastPosition = Vector2.Zero;
		}
	}
}
