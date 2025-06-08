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
		Global.Player.Attributes.Energy.Value += EnergyBonus;
		Global.Player.Attributes.Hunger.Value += HungerBonus;
		Global.Player.Character.PlayerAnim.Play("Use");
		AddNumber(-1);
	}

	public override void Init()
	{
		base.Init();
		AddAction(Consume);
	}
	public Food() { }
}
