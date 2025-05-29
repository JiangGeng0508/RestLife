using Godot;
using System;
using System.Collections.Generic;

public partial class Inventory : Node
{
	public static Dictionary<string, Item> Items = [];
	public override void _Ready()
	{
		Global.Inventory = this;
	}
}