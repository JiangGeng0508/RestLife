using System;
using Godot;

public partial class Item : Node2D
{
	public Vector2I[] Shape { get; set; } = { Vector2I.Zero };
	public Item(){}
}