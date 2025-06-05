using Godot;
using System;

public partial class Hud : CanvasLayer
{
    public Control PlayerState;
    public Control GameWorldInfo;
	
	public Label HealthInfo;
	public Label EnergyInfo;
	public Label HungerInfo;
	public ProgressBar EnergyBar;

	public override void _Ready()
	{
		PlayerState = GetNode<Control>("PlayerState");
		GameWorldInfo = GetNode<Control>("GameWorldInfo");

		HealthInfo = PlayerState.GetNode<Label>("HealthInfo");
		EnergyInfo = PlayerState.GetNode<Label>("EnergyInfo");
		HungerInfo = PlayerState.GetNode<Label>("HungerInfo");
		EnergyBar = PlayerState.GetNode<ProgressBar>("EnergyBar");
		EnergyBar.MaxValue = 100f;
	}

    public override void _Process(double delta)
    {
        HealthInfo.Text = "Health: " + Global.Player.Health.Value.ToString("F1");
        EnergyInfo.Text = "Energy: " + Global.Player.Energy.Value.ToString("F1");
        HungerInfo.Text = "Hunger: " + Global.Player.Hunger.Value.ToString("F1");
		EnergyBar.Value = Global.Player.Energy.Value;

        string formattedTime = $"Day: {Global.GameWorldTime.Days} Time: {Global.GameWorldTime.Hours:D2}:{Global.GameWorldTime.Minutes:00.}";
        GameWorldInfo.GetNode<Label>("Time").Text = formattedTime;
    }
}
