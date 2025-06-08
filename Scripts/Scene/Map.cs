using Godot;
using System;

public partial class Map : Node2D
{

	public override void _Ready()
	{
		Global.Player.Character.Position = GetNode<Node2D>("Buildings/SpawnPoint").Position;
		Global.Player.Camera.Position = Global.Player.Character.Position;
	}
}
