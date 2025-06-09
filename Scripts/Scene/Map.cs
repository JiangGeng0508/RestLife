using Godot;
using System;

public partial class Map : Node2D
{
	public Node2D SpawnPoint;
	public override void _Ready()
	{
		Init();
	}
	public virtual void Init()
	{
		SpawnPoint = GetNode<Node2D>("Buildings/SpawnPoint");
		Global.Player.Character.Position = SpawnPoint.Position;//需要初始场景
		Global.Player.Camera.Position = Global.Player.Character.Position;	
	}
}
