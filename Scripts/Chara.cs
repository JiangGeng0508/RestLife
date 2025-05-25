using Godot;
using System;

public partial class Chara : CharacterBody2D
{
	public override void _Ready()
	{
		base._Ready();
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.IsPressed())
			{
				GD.Print("Right Mouse Button Pressed");
				if (IsOnFloor())
				{
					MoveAndCollide(new Vector2(100, 0));
					GD.Print("move");
				}
			}
		}
	}

}
