using Godot;
using System;

public partial class Cauldron : InteractableItem
{
	public Window window;
	public override void Init()
	{
		window = GetNode<Window>("Window");
		window.Hide();
		window.CloseRequested += () =>
		{
			window.Hide();
		};
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
		}
	}
	public override void Action()
	{
		window.Show();
	}
}
