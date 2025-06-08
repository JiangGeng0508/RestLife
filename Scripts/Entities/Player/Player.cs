using Godot;
using System;

public partial class Player : Node2D
{
	public Character Character;
	public Camera2D Camera;
	public Inventory Inventory;
	public AttributeManager Attributes;

	public override void _Ready()
	{
		Global.Player = this;
		Character = GetNode<Character>("Character");
		Camera = GetNode<Camera2D>("MainCamera");
		Inventory = GetNode<Inventory>("Inventory");
		Attributes = GetNode<AttributeManager>("AttributeManager");
	}
	public void OnSpawn(Variant data)
	{
		
	}
}
