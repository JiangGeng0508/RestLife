using Godot;
using System;

public partial class DressUI : GridContainer
{
	Slot HatSlot;
	Slot ShirtSlot;
	Slot CoatSlot;
	Slot PantsSlot;
	Slot ShoesSlot;
	Slot BagSlot;
	public override void _Ready()
	{
		Global.DressUI = this;
		HatSlot = GetNode<Slot>("HatSlot");
		ShirtSlot = GetNode<Slot>("ShirtSlot");
		CoatSlot = GetNode<Slot>("CoatSlot");
		PantsSlot = GetNode<Slot>("PantsSlot");
		ShoesSlot = GetNode<Slot>("ShoesSlot");
		BagSlot = GetNode<Slot>("BagSlot");
	}
	public void Equip(EquipbleItem item, int slot)
	{

	}
}
