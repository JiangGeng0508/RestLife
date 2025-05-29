using Godot;
using System;

public partial class Dress : GridContainer
{
	Slot HatSlot;
	Slot UpperSlot;
	Slot CoatSlot;
	Slot LowerSlot;
	Slot ShoesSlot;
	Slot BagSlot;
	public override void _Ready()
	{
		Global.Dress = this;
	}
	public void Equip(Item item, int slot)
	{

	}
}
