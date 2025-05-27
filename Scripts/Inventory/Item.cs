using System;
using Godot;

public partial class Item : Node2D
{
	public Vector2[] Shape;
	public Item(Vector2[] shape)
	{
		Shape = shape;
	}
}