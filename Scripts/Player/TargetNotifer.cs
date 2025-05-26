using Godot;
using System;

public partial class TargetNotifer : Node2D
{
	Polygon2D polygon;

	public override void _Ready()
	{
		polygon = GetNode<Polygon2D>("Polygon2D");
		HideTarget();
	}

	public void ShowTarget()
	{
		polygon.Visible = true;
		GetTree().CreateTimer(1.5f).Connect("timeout", new Callable(this, nameof(HideTarget)));
	}
	public void HideTarget()
	{
		polygon.Visible = false;
	}
}
