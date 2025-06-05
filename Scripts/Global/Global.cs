using Godot;
using System;

public partial class Global : Node
{
	public static ResourceLoader ResourceLoader;
	public static Player Player;
	public static GameWorldTime GameWorldTime;
	public static Inventory Inventory;
	public static InventoryUI InventoryUI;
	public static Saver Saver;
	public static EventBus EventBus;

	public override void _Ready()
	{
		GameWorldTime = new();
		AddChild(GameWorldTime);
		Saver = new();
		AddChild(Saver);
		ResourceLoader = new();
		AddChild(ResourceLoader);
		EventBus = new();
	}
}
