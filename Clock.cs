using Godot;
using System;

public partial class Clock : Control
{
	AnimatedSprite2D Weather;
	AnimatedSprite2D Arrow;

	public override void _Ready()
	{
		Weather = GetNode<AnimatedSprite2D>("Weather");
		Arrow = GetNode<AnimatedSprite2D>("Arrow");

		Global.GameWorldTime.OnHourChange += OnHourChanged;
	}
	public void OnHourChanged(int hour)
	{
		if (hour >= 6 && hour < 18)
		{
			Weather.Frame = 0;
			Arrow.Frame = 0;
		}
		else if (hour >= 18 && hour <= 20)
		{
			Weather.Frame = 2;
			Arrow.Frame = 1;
		}
		else
		{
			Weather.Frame = 4;
			Arrow.Frame = 2;
		}
	}
}
