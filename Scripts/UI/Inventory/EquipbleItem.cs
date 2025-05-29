using System;
using Godot;

public enum EquipType
{
	HatSlot,
	ShirtSlot,
	CoatSlot,
	PantsSlot,
	ShoeSlot,
	BagSlot,
	MainHand,
	OffHand
}
public partial class EquipbleItem : Item
{
	[Export(PropertyHint.Enum, "HatSlot,ShirtSlot,CoatSlot,PantsSlot,ShoeSlot,BagSlot,MainHand,OffHand")]
	public EquipType equipType;
	public override void Init()
	{
		AddAction(Equip);
		AddAction(Unequip);
	}
	public void Equip()
	{
		//从Inventory到Dress
		GetParent<Inventory>().EquipToSlot(this, equipType);
	}
	public void Unequip()
	{
		//从Dress到Inventory
		GetParent<Slot>().RemoveChild(this);
		GetNode<Inventory>("../../Inventory}").AddChild(this);
	}
}
