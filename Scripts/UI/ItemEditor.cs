using Godot;
using System;

public partial class ItemEditor : Control
{
	public override void _Ready()
	{
		GetNode<IconPreviewGrid>("IconPreviewGrid").OnButtonPressed += (icon) =>
		{
			GetNode<TextureRect>("Icon").Texture = icon;
		};
	}
}
