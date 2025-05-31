using System;
using Godot;

public partial class Food : Item
{
	public float EnergyBonus { get; set; } = 10f;
	public float HungerBonus { get; set; } = 10f;
	public float ThirstBonus { get; set; } = 5f;

	public void Consume()
	{
		Global.Player.Energy.Value += EnergyBonus;
		Global.Player.Hunger.Value += HungerBonus;
		Global.Player.Thirst.Value += ThirstBonus;
		AddNumber(-1);
	}

	public override void Init()
	{
		AddAction(Consume);
    }

}
