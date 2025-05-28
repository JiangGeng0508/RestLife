using System;
using Godot;

public partial class EquipbleItem : Item
{
	public override void Init()
	{
		AddAction(Equip);
		AddAction(Unequip);
	}
	public virtual void Equip() { }
	public virtual void Unequip() { }
}