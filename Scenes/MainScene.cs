using Godot;
using System;

public partial class MainScene : Node2D
{
	public override void _Ready()
	{
		Global.MainScene = this;
	}
}
