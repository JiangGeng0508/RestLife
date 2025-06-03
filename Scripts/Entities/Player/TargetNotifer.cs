using Godot;
using System;

public partial class TargetNotifer : Node2D
{
	Polygon2D polygon;
	Timer timer;

	public override void _Ready()
	{
		polygon = GetNode<Polygon2D>("Polygon2D");
		timer = GetNode<Timer>("Timer");
		timer.Timeout += HideTarget;
		HideTarget();
	}

	public void ShowTarget()
	{
		polygon.Visible = true;
		timer.Start();
	}
	public void HideTarget()
	{
		polygon.Visible = false;
		timer.Stop();
	}
}
