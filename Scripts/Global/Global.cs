using Godot;
using System;

public partial class Global : Node
{
	public static Player Player;
	public static GameWorldTime GameWorldTime;
	public static InventoryUI InventoryUI;
	public static MainScene MainScene;
	public static NeoInventory NeoInventory;

	// Rename This
	public static SelectMenu SelectMenu;

	public override void _Ready()
	{
		GameWorldTime = new();
		AddChild(GameWorldTime, true);
		
		ItemManager.Init();
		EventBus.Init();
		RecipeManager.Init();
	}
}
