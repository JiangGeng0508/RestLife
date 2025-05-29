using System;
using System.Linq;
using Godot;

public partial class Item : GodotObject
{
	public String Name { get; set; } = "None";
	public Action[] actions = new Action[9];
	private int _number = 1;
	[Export(PropertyHint.Range, "0,99,1")]
	public int Number
	{
		get => _number;
		set
		{
			if (value > 0) _number = value;
			// else Delete();
		}
	}
	public Item()
	{

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
