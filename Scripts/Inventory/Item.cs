using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	public Action[] actions;
	public override void _Ready()
	{
		//Default actions
		actions.Append(Suicide);

		AddActions();
	}
	public Item() { }
	public void Suicide()
	{
		RemoveFromGroup("Inventory");
		QueueFree();
	}
	public virtual void AddActions() { }
}