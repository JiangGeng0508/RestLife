using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	public Action[] actions;
	Inventory inventory;
	public override void _Ready()
	{
		inventory = GetParent<Inventory>();
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
		inventory.InventoryUpdate();
	}
	public void AddAction(Action action)
	{
		actions = actions.Append(action).ToArray();
	}
	public virtual void Init() { }
}
