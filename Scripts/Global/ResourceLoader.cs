using System;
using Godot;

public partial class ResourceLoader : Node
{
	public Sprite2D NoneSprite;
	public static T Load<T>(string path) where T : Resource
	{
		return GD.Load(path) as T;
	}
}