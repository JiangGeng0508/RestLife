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
		//注册所有Item
		using var dir = DirAccess.Open("res://Asset/Data/Items/");
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			GD.Print("Loading item: " + file);
			var item = GD.Load<Item>("res://Asset/Data/Items/" + file);
			Register(item);
		}
		//从Texture创建Food
		using var dir2 = DirAccess.Open("res://Asset/Sprite/Food/Food/");
		var files2 = dir2.GetFiles();
		foreach (var file in files2)
		{
			if (file.GetExtension() != "png") continue;
			GD.Print("Creating item from texture: " + file);
			var item = ShapeFoodFromTexture("res://Asset/Sprite/Food/Food/" + file);
			item.Name = file.GetBaseName().Split('.')[0];
			Register(item);
			// Export(item);
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
	public Food ShapeFoodFromTexture(string texturePath)
	{
		var item = new Food();
		item.Icon = GD.Load<Texture2D>(texturePath);
		return item;
	}
	public void Export(Food food)
	{
		var path = "res://Asset/Data/Items/Export/" + food.Name + ".tres";
		ResourceSaver.Save(food, path);
	}
}
