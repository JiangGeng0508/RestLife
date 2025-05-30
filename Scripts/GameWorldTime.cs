using Godot;
using System;

public partial class GameWorldTime : Node2D
{
    public float Seconds;
    public float Minutes;
    public float Hours;
    public float Days;

    public override void _Ready()
    {
        Global.GameWorldTime = this;
    }

    public override void _Process(double delta)
    {
        Seconds += (float)delta;
        if (Seconds >= 2)
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
