using System;
using Godot;
using Godot.Collections;

//AutoLoad Script
public partial class Saver : Node
{
	public SceneTree MainSceneTree;
	private static readonly string SavePath = "res://savegame.save";

	public override void _Ready()
	{
		Global.Saver = this;
	}
	public static void Save()
	{
		GD.Print("Saving game...");
		//Player
		//Time
		//Inventory
		//Dress
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
