using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	public Action[] actions;
	private int number = 1;
	[Export(PropertyHint.Range, "0,99,1")]
	public int Number
	{
		get => number;
		set
		{
			if (value > 0) number = value;
			else Delete();
		}
	}
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
		GetParent<Inventory>().InventoryUpdate();
	}
	public void AddAction(Action action)
	{
		actions = actions.Append(action).ToArray();
	}
	public virtual void Init() { }
}
