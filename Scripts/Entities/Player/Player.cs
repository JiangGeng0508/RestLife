using Godot;
using System;

public partial class Player : Node2D
{
	public Character Character;
	public Inventory Inventory;

	public override void _Ready()
	{
		Global.Player = this;
		Character = GetNode<Character>("Character");
		Inventory = GetNode<Inventory>("Inventory");
	}
	public void OnSpawn(Variant data)
	{
		
	}
}
