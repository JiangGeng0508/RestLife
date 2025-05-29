using System;
using Godot;

public partial class Dress : Node
{
	public override void _Ready()
	{
		Global.Dress = this;
	}
}
