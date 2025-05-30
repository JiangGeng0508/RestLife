using Godot;
using System;

public partial class Hud : CanvasLayer
{
    public Control PlayerState;

    public override void _Ready()
    {
        PlayerState = GetNode<Control>("PlayerState");
    }

    public override void _Process(double delta)
    {
        PlayerState.GetNode<Label>("HealthInfo").Text = "Health: " + Global.Player.Health.ToString();
        PlayerState.GetNode<Label>("EnergyInfo").Text = "Energy: " + Global.Player.Energy.ToString();
        PlayerState.GetNode<Label>("HungerInfo").Text = "Hunger: " + Global.Player.Hunger.ToString();
        PlayerState.GetNode<Label>("ThirstInfo").Text = "Thirst: " + Global.Player.Thirst.ToString();
    }
}
