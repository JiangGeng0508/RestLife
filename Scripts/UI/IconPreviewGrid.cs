using Godot;
using System;

public partial class IconPreviewGrid : GridContainer
{
	[Export]
	public string IconPath { get; set; } = "res://Asset/Sprite/Food/FastFood/";
	[Signal]
	public delegate void OnButtonPressedEventHandler(Texture2D icon);

	public override void _Ready()
	{
		using var dir = DirAccess.Open(IconPath);
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			if (file.EndsWith(".import"))
			{
				continue;
			}
			var icon = GD.Load<Texture2D>(IconPath + file);
			var button = new Button
			{
				Icon = icon,
				CustomMinimumSize = new Vector2(100, 100),
				IconAlignment = HorizontalAlignment.Center,
				ExpandIcon = true,
			};
			button.Pressed += () =>
			{
				EmitSignal(nameof(OnButtonPressed), icon);
			};
			AddChild(button);
		}
	}
}
