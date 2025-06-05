using System;
using Godot;
[GlobalClass]
public partial class Food : Item
{
	[Export]
	public float EnergyBonus { get; set; } = 10f;
	[Export]
	public float HungerBonus { get; set; } = 10f;

	public void Consume()
	{
		Global.Player.Energy.Value += EnergyBonus;
		Global.Player.Hunger.Value += HungerBonus;
		AddNumber(-1);
	}

	public override void Init()
	{
		AddAction(Consume);
	}
	public Food() { }
	public Food(string name, Texture2D icon, float energyBonus, float hungerBonus)
	{

	}
}
