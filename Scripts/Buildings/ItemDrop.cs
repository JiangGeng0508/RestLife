using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	[Export]
	public Item Item;

	public int Number;

	public Sprite2D Icon;

	public RichTextLabel Hint;

	[Export(PropertyHint.ColorNoAlpha)]
	public Color HintColor = new Color(0.5f, 0.8f, 0.1f);

	private Tween tween;

	public override void Init()
	{
		tween = CreateTween();
		Hint = GetNode<RichTextLabel>("Hint");
		string ItemName = Item.Number > 1 ? (Item.Name + " x" + Item.Number) : Item.Name;
		Hint.Text = "[color=" + HintColor.ToHtml() + "]" + "E to pick up " + ItemName + "[/color]";
		Icon = GetNode<Sprite2D>("Icon");

		if (Item != null)
		{
			Icon.Texture = Item.Icon;
			Name = Item.Name;
			Number = Item.Number;
		}

		// 初始化浮动动画
		StartFloatingAnimation();
	}

	private void StartFloatingAnimation()
	{
		float startY = 0;

		tween.TweenProperty(Hint, "position:y", startY, 1f);
		tween.TweenProperty(Hint, "position:y", startY - 12.5f, 1f);
		tween.SetLoops(-1);
		tween.Play();
	}

	public override void Action()
	{
		GD.Print("Pick up " + Item.Name);
		Global.Player.Inventory.Call("AddItem", Item, Number);
		QueueFree();
	}

	public ItemDrop()
	{
		// Render Index
		ZIndex = 100;
	}

	public void ToggleHint(bool show)
	{
		if (show)
		{
			Hint.Visible = true;
		}
		else
		{
			Hint.Visible = false;
		}
	}
}
