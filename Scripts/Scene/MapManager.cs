using Godot;
using System;

public partial class MapManager : Node
{
	Vector2 lastPosition = Vector2.Zero;
	public void Teleport(string mapName)
	{
		if (GetChild(0) is Map map)
		{
		map.SpawnPoint.Position = Global.Player.Character.Position;
		}
		ChangeMap(mapName);
	}
	public void ChangeMap(string mapName)
	{
		if (GetChildCount() == 0)
		{
			AddChild(GD.Load<PackedScene>("res://Scenes/Scene/TestMap2.tscn").Instantiate());
		}
		var preChildren = GetChildren();
		foreach (var child in preChildren)
		{
			RemoveChild(child);
		}
		AddChild(GD.Load<PackedScene>("res://Scenes/Scene/" + mapName + ".tscn").Instantiate());
	}
}
