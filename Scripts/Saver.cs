using System;
using Godot;
using Godot.Collections;

//AutoLoad Script
public partial class Saver : Node
{
	private static readonly string SavePath = "res://savegame.save";

	public override void _Ready()
	{
		Global.Saver = this;
	}
	public static void Save()
	{
		GD.Print("Saving game...");
		using var file = FileAccess.Open(SavePath, FileAccess.ModeFlags.Write);
		var saveData = new Dictionary();
		saveData["Player"] = Global.Player;
		saveData["GameWorldTime"] = Global.GameWorldTime;
		saveData["Dress"] = Global.Dress;
		file.StoreVar(saveData.Duplicate());
		file.Close();
		GD.Print("Game saved.");
	}
	public static void SaveScene(Node node)
	{
		var scene = new PackedScene();
		if (scene.Pack(node) == Error.Ok)
		{
			Error error = ResourceSaver.Save(scene, $"res://Save/{node.Name}.tscn");
			if (error != Error.Ok)
			{
				GD.PushError($"An error occurred while saving the scene: {error}");
			}
		}
	}
}
