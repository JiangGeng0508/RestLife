using System;
using Godot;
using Godot.Collections;

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
		var scene = new PackedScene();
		var saveData = new Dictionary();
		saveData["Player"] = Global.Player;
		saveData["GameWorldTime"] = Global.GameWorldTime;
		saveData["Inventory"] = (Variant)Global.Inventory;
		saveData["Dress"] = (Variant)Global.Dress;
		file.StoreVar(saveData.Duplicate());
		file.Close();
	}
}