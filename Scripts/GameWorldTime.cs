using Godot;
using System;

//AutoLoad Script
public partial class GameWorldTime : Node
{
	[Signal]
	public delegate void OnDayChangeEventHandler(int days);
	public float Seconds = 0;
	public int Minutes = 0;
	public int Hours = 6;
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
		OnDayChange += days => { GD.Print("Day changed to " + Days);};
	}

	public override void _Process(double delta)
	{
		Seconds += (float)delta;
	}
}
