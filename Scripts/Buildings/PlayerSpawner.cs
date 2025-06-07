using Godot;
using System;

public partial class PlayerSpawner : MultiplayerSpawner
{
	public override void _Ready()
	{
		SpawnFunction = new Callable(this,nameof(SpawnPlayer));
		Spawn();
		// if (Global.Player == null)
		// {
		// 	AddChild(GD.Load<PackedScene>("res://Scenes/Entities/Player/Player.tscn").Instantiate<Player>());
		// }
	}
	private Player SpawnPlayer(Variant data)
	{
		return GD.Load<PackedScene>("res://Scenes/Entities/Player/Player.tscn").Instantiate<Player>();
	}
}
