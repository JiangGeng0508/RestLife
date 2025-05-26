using System;
using Godot;
public enum ItemType
{
	Food,
	Tool
}
public partial class Item : Object 
{
	public string Name { get; set; }
	public string Description { get; set; }
	public float Price { get; set; }
	public string SpritePath { get; set; }
	public ItemType Type { get; set; }
	public Item(string name, string description, float price, string spritePath, ItemType type)
	{
		Name = name;
		Description = description;
		Price = price;
		SpritePath = spritePath;
		Type = type;
	}
}