using System;
using Godot;

public partial class Food : Item
{
	public float EnergyBonus { get; set; } = 0.0f;
	public float HungerBonus { get; set; } = 0.0f;
	public float ThirstBonus { get; set; } = 0.0f;

	public void Consume()
	{
		Global.Player.Energy += EnergyBonus;
		Global.Player.Hunger += HungerBonus;
		Global.Player.Thirst += ThirstBonus;
		AddNumber(-1);
	}

	public override void Init()
	{
		EnergyBonus = 10.0f;
		HungerBonus = 10.0f;
		ThirstBonus = -5.0f;
		AddAction(Consume);
    }

}