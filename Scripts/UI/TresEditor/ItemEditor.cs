using Godot;
using System;

public partial class ItemEditor : Control
{
	[Export]
	public string ExportPath { get; set; } = "res://Asset/Data/Items/Export/";
	public string ItemName { get; set; }
	public Texture2D ItemIcon { get; set; }

	public override void _Ready()
	{
		GetNode<IconPreviewGrid>("Scroll/IconPreviewGrid").OnButtonPressed += (icon) =>
		{
			GetNode<TextureRect>("Icon").Texture = icon;
		};
		GetNode<Button>("Check").Pressed += CheckItem;
	}
	public virtual void CheckItem()
	{
		ItemName = GetNode<LineEdit>("Name").Text;
		ItemIcon = GetNode<TextureRect>("Icon").Texture;
		if (ItemName.Length > 0 && ItemIcon != null)
		{
			var item = new Item()
			{
				Name = ItemName,
				Icon = ItemIcon,
			};
			var path = ExportPath + item.Name + ".tres";
			ResourceSaver.Save(item, path);
			GetNode<Label>("Warning").Text = "Item Created";
		}
		else
		{
			GetNode<Label>("Warning").Text = "Create Failed";
		}
	}
}
