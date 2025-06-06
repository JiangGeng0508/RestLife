using Godot;
using System;

public partial class Global : Node
{
	public static ResourceLoader ResourceLoader;
	public static Player Player;
	public static GameWorldTime GameWorldTime;
	public static InventoryUI InventoryUI;
	public static Saver Saver;
	public static EventBus EventBus;
	public static ItemManager ItemManager;
	public static MainScene MainScene;

	public override void _Ready()
	{
		GameWorldTime = new();
		AddChild(GameWorldTime, true);
		Saver = new();
		AddChild(Saver, true);
		ResourceLoader = new();
		AddChild(ResourceLoader, true);
		EventBus = new();
		AddChild(EventBus, true);
		ItemManager = new();
		AddChild(ItemManager, true);
	}
}
