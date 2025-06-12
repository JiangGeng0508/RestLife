using Godot;
using System;

public partial class MainScene : Node2D
{
	public MapManager MapManager;
	public BgmPlayer BgmPlayer;
	public override void _Ready()
	{
		Global.MainScene = this;
		MapManager = GetNode<MapManager>("MapManager");
		BgmPlayer = GetNode<BgmPlayer>("BGMPlayer");
	}
}
