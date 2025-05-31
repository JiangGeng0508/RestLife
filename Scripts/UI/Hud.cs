using Godot;
using System;

public partial class Hud : CanvasLayer
{
    public Control PlayerState;
    public Control GameWorldInfo;
	
	public Label HealthInfo;
	public Label EnergyInfo;
	public Label HungerInfo;
	public Label ThirstInfo;
	public ProgressBar EnergyBar;

	public override void _Ready()
	{
		PlayerState = GetNode<Control>("PlayerState");
		GameWorldInfo = GetNode<Control>("GameWorldInfo");

		HealthInfo = PlayerState.GetNode<Label>("HealthInfo");
		EnergyInfo = PlayerState.GetNode<Label>("EnergyInfo");
		HungerInfo = PlayerState.GetNode<Label>("HungerInfo");
		ThirstInfo = PlayerState.GetNode<Label>("ThirstInfo");
		EnergyBar = PlayerState.GetNode<ProgressBar>("EnergyBar");
		EnergyBar.MaxValue = 100f;
	}

    public override void _Process(double delta)
    {
        HealthInfo.Text = "Health: " + Global.Player.Health.ToString();
        EnergyInfo.Text = "Energy: " + Global.Player.Energy.ToString();
        HungerInfo.Text = "Hunger: " + Global.Player.Hunger.ToString();
        ThirstInfo.Text = "Thirst: " + Global.Player.Thirst.ToString();
		EnergyBar.Value = Global.Player.Energy;

        string formattedTime = $"Day: {Global.GameWorldTime.Days} Time: {Global.GameWorldTime.Hours.ToString("D2")}:{Global.GameWorldTime.Minutes.ToString("D2")}";
        GameWorldInfo.GetNode<Label>("Time").Text = formattedTime;
    }
}
