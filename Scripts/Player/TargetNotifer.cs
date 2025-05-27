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
	}
	public void HideTarget()
	{
		polygon.Visible = false;
	}
}
