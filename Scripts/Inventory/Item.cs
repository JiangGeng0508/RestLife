using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	public Action[] actions;
	public override void _Ready()
	{
		//Default actions
		actions = new Action[1];
		AddAction(Suicide);
		GD.Print("Item Ready");
		Init();
	}
	public Item()
	{
		Name = "None";
	}
	public void Suicide()
	{
		RemoveFromGroup("Inventory");
		QueueFree();
	}
	public void AddAction(Action action)
	{
		actions = actions.Append(action).ToArray();
	}
	public virtual void Init() { }
}
