using Godot;
using System;

public partial class MainCamera : Camera2D
{
	Character chara;
	public override void _Ready()
	{
		chara = GetNode<Character>("../Character");
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Position.DistanceTo(chara.Position) > 10f)
		{
			Position += (chara.Position - Position) / 2 * 5 *(float)delta;
		}
	}
}
