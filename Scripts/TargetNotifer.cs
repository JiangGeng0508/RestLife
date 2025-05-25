using Godot;
using System;

public partial class TargetNotifer : Node2D
{
	Sprite2D sprite1;
	Sprite2D sprite2;

	public override void _Ready()
	{
		sprite1 = GetNode<Sprite2D>("Sprite1");
		sprite2 = GetNode<Sprite2D>("Sprite2");
		HideTarget();
	}

	public void ShowTarget()
	{
		sprite1.Visible = true;
		sprite2.Visible = true;
		GetTree().CreateTimer(1.5f).Connect("timeout", new Callable(this, nameof(HideTarget)));
	}
	public void HideTarget()
	{
		sprite1.Visible = false;
		sprite2.Visible = false;
	}
}
