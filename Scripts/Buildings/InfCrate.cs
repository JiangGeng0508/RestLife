using Godot;
using System;

public partial class InfCrate : InteractableItem
{
	public GridContainer Grid;
	public Window window;
	public override void Init()
	{
		window = GetNode<Window>("Window");
		window.Hide();
		window.FocusExited += () =>
		{
			window.Hide();
		};
		window.CloseRequested += () =>
		{
			window.Hide();
		};
		Grid = GetNode<GridContainer>("Window/InfItemGrid");
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
		window.Show();
	}
}
