using Godot;
using System;

public partial class GameWorldTime : Node
{
	[Signal]
	public delegate void OnDayChangeEventHandler(int days);
	private float _minutes = 0;
	public float Minutes
	{
		get { return _minutes; }
		set
		{
			if (Minutes >= 60)
			{
				_minutes = 0;
				Hours++;
			}
			else
			{
				_minutes = value;
			}
		}
	}
	private int _hours = 6;
	public int Hours
	{
		get { return _hours; }
		set
		{
			if (value >= 24)
			{
				_hours = 0;
				Days++;
			}
			else
			{
				_hours = value;
			}
		}
	}
	private int _days = 1;
	public int Days
	{
		get { return _days; }
		set
		{
			_days = value;
			EmitSignal(nameof(OnDayChange), Days);
		}
	}

	public override void _Ready()
	{
		Global.GameWorldTime = this;
		OnDayChange += days => { GD.Print($"{days} days.");};
	}

	public override void _Process(double delta)
	{
		Minutes += (float)delta;
	}
}
