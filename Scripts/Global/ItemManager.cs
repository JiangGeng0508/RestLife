using System;
using System.Collections.Generic;
using Godot;

public static class ItemManager
{
	public static List<Item> RegisteredItems { get; set; } = new List<Item>();
	public static Dictionary<string, Item> ItemDict { get; set; } = new Dictionary<string, Item>();

	public static void Init()
	{
		//注册所有Item
		using var dir = DirAccess.Open("res://Asset/Data/Items/");
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			var item = GD.Load<Item>("res://Asset/Data/Items/" + file);
			Register(item);
		}
	}
	public static void Register(Item item)
	{
		RegisteredItems.Add(item);
		ItemDict[item.Name] = item;
	}
	public static void Register(params Item[] items)
	{
		foreach (var item in items)
		{
			RegisteredItems.Add(item);
			ItemDict[item.Name] = item;
		}
	}
	public static void ImportTexture(string dictPath)
	{
		//从Texture创建Food
		using var dir2 = DirAccess.Open(dictPath);//以'/'结尾
		var files2 = dir2.GetFiles();
		foreach (var file in files2)
		{
			if (file.GetExtension() != "png") continue;
			var item = ShapeFoodFromTexture(dictPath + file);
			item.Name = file.GetBaseName().Split('.')[0];
			Register(item);
			// Export(item);
		}
	}
	public static Food ShapeFoodFromTexture(string texturePath)
	{
		var item = new Food();
		item.Icon = GD.Load<Texture2D>(texturePath);
		return item;
	}
	public static void Export(Food food)
	{
		var path = "res://Asset/Data/Items/Export/" + food.Name + ".tres";
		ResourceSaver.Save(food, path);
	}
}
