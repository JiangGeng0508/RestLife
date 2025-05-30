using Godot;
using System;

public partial class Hud : CanvasLayer
{
    public Control PlayerState;
    public Control GameWorldInfo;

    public override void _Ready()
    {
        PlayerState = GetNode<Control>("PlayerState");
        GameWorldInfo = GetNode<Control>("GameWorldInfo");
    }

    public override void _Process(double delta)
    {
        PlayerState.GetNode<Label>("HealthInfo").Text = "Health: " + Global.Player.Health.ToString();
        PlayerState.GetNode<Label>("EnergyInfo").Text = "Energy: " + Global.Player.Energy.ToString();
        PlayerState.GetNode<Label>("HungerInfo").Text = "Hunger: " + Global.Player.Hunger.ToString();
        PlayerState.GetNode<Label>("ThirstInfo").Text = "Thirst: " + Global.Player.Thirst.ToString();

        GameWorldInfo.GetNode<Label>("Time").Text = "Day: " + Global.GameWorldTime.Days.ToString() + " Time: " + Global.GameWorldTime.Hours.ToString() + ":" + Global.GameWorldTime.Minutes.ToString() + ":" + Global.GameWorldTime.Seconds.ToString();
    }
}
