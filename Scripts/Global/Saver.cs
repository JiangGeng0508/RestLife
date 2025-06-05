using System;
using Godot;
using Godot.Collections;

public partial class Saver : Node
{
	public SceneTree MainSceneTree;
	private static readonly string SavePath = "res://savegame.save";

	public static void Save()
	{
		GD.Print("Saving game...");
		using var file = FileAccess.Open("SavePath", FileAccess.ModeFlags.Write);
		Dictionary<string, float> playerAttributes = new Dictionary<string, float>
		{
			{ "Health", Global.Player.Health.Value },
			{ "Energy", Global.Player.Energy.Value },
			{ "Hunger", Global.Player.Hunger.Value }
		};
		Dictionary<string, int> gameTime = new Dictionary<string, int>
		{
			{ "Day", Global.GameWorldTime.Days },
			{ "Hour", Global.GameWorldTime.Hours }
		};
		//Inventory
		Dictionary<string, Item> inventoryData = new Dictionary<string, Item>();
		//Event
		//Scene
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
