using System;
using Godot;

public enum ItemTypeEnum
{
	Tool,
	Raw,
	Material,
	Prop,
	Misc
}
public enum RarityEnum
{
	Common,
	Rare
}
public partial class ItemData : Resource
{
	[Export] public string ItemName { get; private set; }
    [Export] public Texture2D ItemIcon { get; private set; }
    [Export] public bool Stackable { get; private set; }

    public virtual void OnItemSelected(Node inventoryOwner, Inventory inventory)
    {

    }
}