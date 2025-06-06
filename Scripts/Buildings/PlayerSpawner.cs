using Godot;
using System;

public partial class PlayerSpawner : Node2D
{
	public override void _Ready()
	{
		if (Global.Player == null)
		{
			AddChild(GD.Load<PackedScene>("res://Scenes/Entities/Player/Player.tscn").Instantiate<Player>());
		}
	}
}
