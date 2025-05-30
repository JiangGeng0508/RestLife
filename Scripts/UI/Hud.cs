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

        string formattedTime = $"Day: {Global.GameWorldTime.Days} Time: {Global.GameWorldTime.Hours.ToString("D2")}:{Global.GameWorldTime.Minutes.ToString("D2")}";
        GameWorldInfo.GetNode<Label>("Time").Text = formattedTime;
    }
}
