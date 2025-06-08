using System;
using Godot;
using Godot.Collections;

public partial class Saver : Node
{
	public SceneTree MainSceneTree;
	//private static readonly string SavePath = "res://savegame.save";

	public override void _Ready()
	{
		Global.Saver = this;
	}
	public static void Save()
	{
		GD.Print("Saving game...");
		Dictionary<string, float> playerAttributes = new Dictionary<string, float>();
		playerAttributes.Add("Health", Global.Player.Attributes.Health.Value);
		playerAttributes.Add("Energy", Global.Player.Attributes.Energy.Value);
		playerAttributes.Add("Hunger", Global.Player.Attributes.Hunger.Value);
		Dictionary<string, int> gameTime = new Dictionary<string, int>();
		gameTime.Add("Day", Global.GameWorldTime.Days);
		gameTime.Add("Hour", Global.GameWorldTime.Hours);
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
