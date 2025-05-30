using System;
using Godot;

public partial class ResourceLoader : Node
{
	public Sprite2D NoneSprite;
	public override void _Ready()
	{
		Global.ResourceLoader = this;
		NoneSprite = GD.Load<Sprite2D>("res://icon.svg");
	}

	public static T Load<T>(string path) where T : Resource
	{
		return GD.Load(path) as T;
	}
}