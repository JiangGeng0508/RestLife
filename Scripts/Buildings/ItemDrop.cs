using Godot;
using System;

public partial class ItemDrop : InteractableItem
{
	[Export]
	public Item Item;

	public Sprite2D Icon;

	public RichTextLabel Hint;

	[Export(PropertyHint.ColorNoAlpha)]
	public Color HintColor { get; set; }
	[Export(PropertyHint.ColorNoAlpha)]
	public Color EColor { get; set; }

	private Tween tween;

	public override void Init()
	{
		Icon = GetNode<Sprite2D>("Icon");
		Hint = GetNode<RichTextLabel>("Hint");
		tween = CreateTween();

		if (Item != null)
		{
			Icon.Texture = Item.Icon;
			Name = Item.Name;
			Hint.Text = $"[color={EColor.ToHtml()}][E][/color] [color={HintColor.ToHtml()}]to pick up {Name}[/color]";
		}
		// 初始化浮动动画
		StartFloatingAnimation();
	}

	private void StartFloatingAnimation()
	{
		tween.TweenProperty(Hint, "position:y", 0, 1f);
		tween.TweenProperty(Hint, "position:y", -12.5f, 1f);
		tween.SetLoops(-1);
		tween.Play();
		ToggleHint(false);
	}

	public override void Action()
	{
		GD.Print("Pick up " + Item.Name);
		Global.Player.Inventory.Call("AddItem", Item , 1);
		QueueFree();
	}

	public ItemDrop()
	{
		// Render Index
		ZIndex = 100;
	}

	public void ToggleHint(bool show) => Hint.Visible = show;
}
