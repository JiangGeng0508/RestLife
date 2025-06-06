using Godot;
using System;

public partial class InfCrate : InteractableItem
{
	public GridContainer Grid;
	public override void Init()
	{
		Grid = GetNode<GridContainer>("InfItemGrid");
		foreach (Item item in Global.ItemManager.RegisteredItems)
		{
			var button = new Button()
			{
				Name = item.Name,
				Icon = item.Icon,
				Text = item.Name,
			};
			button.Pressed += () =>
			{
				Global.Player.Inventory.AddItem(item, 1);
			};
			Grid.AddChild(button);
		}
	}
	public override void Action()
	{
		Grid.Show();
	}
}
