using System;
using Godot;

[GlobalClass]
public partial class Recipe : Resource
{
	[Export]
	public Item[] Ingredients { get; set; }
	[Export]
	public Item Result { get; set; }
	// public int Time { get; set; }
	public Recipe()
	{
	}
}