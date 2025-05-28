using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	[Signal] public delegate void InventoryUpdateEventHandler();
	public Action[] actions;
	public override void _Ready()
	{
		//Default actions
		actions = new Action[9];
		AddAction(Delete);
		Init();
	}
	public Item()
	{
		Name = "None";
	}
	public void Delete()
	{
		RemoveFromGroup("Inventory");
		QueueFree();
		EmitSignal(nameof(InventoryUpdateEventHandler));
	}
	public void AddAction(Action action)
	{
		actions = actions.Append(action).ToArray();
	}
	public virtual void Init() { }
}
