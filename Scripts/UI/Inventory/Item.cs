using System;
using System.Linq;
using Godot;

public partial class Item : Node2D
{
	public Action[] actions = new Action[9];
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
	public string ItemName { get; set; }

	public override void _Ready()
	{
		//Default actions
		AddAction(Delete);
		Init();
	}
	public Item()
	{
		// Why is this here?
		// Name = "None";
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
	public void AddNumber(int number)
	{
		Number += number;
	}
	public virtual void Init() { }
}
