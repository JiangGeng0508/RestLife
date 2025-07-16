using System;
using Godot;

[GlobalClass]
public partial class Recipe : Resource
{
	//合成原料3x3
	//1 2 3
	//4 5 6
	//7 8 9
	[Export]
	public Item[] Ingredients { get; set; } = new Item[9];
	[Export]
	public Item Result { get; set; }
	public int Time { get; set; } = 0;
	public Recipe() { }
	public Recipe(Item[] ingredients, Item result)
	{
		Ingredients = ingredients;
		Result = result;
	}
}