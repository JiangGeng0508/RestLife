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
		ReadFolder(IconPath);
	}
	public void ReadFolder(string path)
	{
		foreach (var child in GetChildren())
		{
			child.QueueFree();
		}
		using var dir = DirAccess.Open(path);
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			if (!file.EndsWith(".png"))
			{
				continue;
			}
			var icon = GD.Load<Texture2D>(path + "/" +file);
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
