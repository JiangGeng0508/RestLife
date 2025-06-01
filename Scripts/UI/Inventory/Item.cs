using System;
using System.Linq;
using Godot;

public partial class Item : GodotObject
{
	public String Name { get; set; } = "None";//标识符，不能重复
	public Texture2D Icon { get; set; } = GD.Load<Texture2D>("res://icon.svg");//图标
	public Action[] actions = new Action[9];

	private int _number = 1;
	[Export(PropertyHint.Range, "0,99,1")]
	public int Number
	{
		get
		{
			return _number;
		}
		set
		{
			_number = value;
			Global.InventoryUI.Update();
			if (_number <= 0)
			{
				Delete();
			}
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
	public void Delete()
	{
		Inventory.RemoveItem(this);
	}
	public virtual void Init() { }
	
}
