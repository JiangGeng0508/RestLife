using Godot;
using System;

public partial class Global : Node
{
	public static Player Player;
	public static GameWorldTime GameWorldTime;
	public static InventoryUI InventoryUI;
	public static ItemManager ItemManager;
	public static MainScene MainScene;

	// Rename This
	public static SelectMenu SelectMenu;

	public override void _Ready()
	{
		GameWorldTime = new();
		AddChild(GameWorldTime, true);
		ItemManager = new();
		AddChild(ItemManager, true);

		EventBus.Init();
		RecipeManager.Init();
	}
}
