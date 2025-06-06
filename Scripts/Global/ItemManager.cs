using System;
using System.Collections.Generic;
using Godot;

public partial class ItemManager : Node
{
	public List<Item> RegisteredItems { get; set; } = [];
	public override void _Ready()
	{
		Global.ItemManager = this;
		GD.Print("ItemManager is ready");
		using var dir = DirAccess.Open("res://Asset/Data/Items/");
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			GD.Print("Loading item: " + file);
			var item = GD.Load<Item>("res://Asset/Data/Items/" + file);
			Register(item);
		}
	}
	public void Register(Item item)
	{
		RegisteredItems.Add(item);
	}
	public void Register(params Item[] items)
	{
		foreach (var item in items)
		{
			RegisteredItems.Add(item);
		}
	}
}
