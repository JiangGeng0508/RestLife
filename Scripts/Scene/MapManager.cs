using Godot;
using System;

public partial class MapManager : Node
{
	public void ChangeMap(string mapName)
	{
		if (GetChildCount() == 0)
		{
			GD.Print("No map found!");
			return;
		}
		var preChildren = GetChildren();
		foreach (var child in preChildren)
		{
			RemoveChild(child);
		}

		var newMapInstance = GD.Load<PackedScene>("res://Scenes/Scene/" + mapName + ".tscn").Instantiate();
		AddChild(newMapInstance);
	}
}
