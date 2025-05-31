using Godot;
using System;

public partial class GameWorldTime : Node
{
    public float Seconds;
    public int Minutes;
    public int Hours;
    public int Days;

    public override void _Ready()
    {
        Global.GameWorldTime = this;
    }

    public override void _Process(double delta)
    {
        Seconds += (float)delta;
        if (Seconds >= 1.5)
        {
            Seconds = 0;
            Minutes++;
        }
        if (Minutes >= 60)
        {
            Minutes = 0;
            Hours++;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }
}
