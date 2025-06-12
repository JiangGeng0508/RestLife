using Godot;
using System;

public partial class Global : Node
{
	public static Player Player;
	public static GameWorldTime GameWorldTime;
	public static InventoryUI InventoryUI;
	public static EventBus EventBus;
	public static ItemManager ItemManager;
	public static MainScene MainScene;
	public static RecipeManager RecipeManager;

	public override void _Ready()
	{
		GameWorldTime = new();
		AddChild(GameWorldTime, true);
		EventBus = new();
		AddChild(EventBus, true);
		ItemManager = new();
		AddChild(ItemManager, true);
		RecipeManager = new();
		AddChild(RecipeManager, true);
	}
}
