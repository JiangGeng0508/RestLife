using Godot;
using System;
using System.Collections.Generic;
public partial class Inventory : ItemList
{
	private const int _ButtonSize = 50;
	private Texture2D _TextureNormal;
	public LinkedList<Item> Items = new LinkedList<Item>();
	public override void _Ready()
	{
		_TextureNormal = ResourceLoader.Load("res://icon.svg") as Texture2D;
		GenerateVoidButton();
	}
	public void AddItem(Item item)
	{
		Items.AddLast(item);
	}
	public void GenerateButton(Item item)
	{
		var Item = new Node2D
		{
			Name = item.Name
		};
		AddChild(Item);
		Vector2I[] shape = item.Shape;
		foreach (Vector2I pos in shape)
		{
			ItemButton button = new ItemButton
			{
				TextureNormal = _TextureNormal,
				Position = pos * _ButtonSize,
				Size = new Vector2(_ButtonSize, _ButtonSize)
				
			};
			Item.AddChild(button);
		}
	}
	//debug
	public void GenerateVoidButton()
	{
		var Item = new Item
		{
			Name = "Void",
			Shape = new Vector2I[] { Vector2I.Zero , Vector2I.Left, Vector2I.Right, Vector2I.Up }
		};
		GenerateButton(Item);
	}
}
