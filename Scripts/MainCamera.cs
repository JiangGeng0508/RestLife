using Godot;
using System;

public partial class MainCamera : Camera2D
{
	Chara chara;
	public override void _Ready()
	{
		chara = GetNode<Chara>("../Chara");
	}
	public override void _PhysicsProcess(double delta)
	{
		if (Position.DistanceTo(chara.Position) > 10f)
		{
			Position += (chara.Position - Position) / 2 * 5 *(float)delta;
		}
	}
}
