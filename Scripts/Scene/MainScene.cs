using Godot;
using System;

public partial class MainScene : Node2D
{
	public MapManager MapManager;
	public override void _Ready()
	{
		Global.MainScene = this;
		MapManager = GetNode<MapManager>("MapManager");
	}
}
