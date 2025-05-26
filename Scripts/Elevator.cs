using Godot;
using System;

public partial class Elevator : InteractableItem
{
	[Export(PropertyHint.Enum, "Up:-1,Down:1")]
	public int Direction { get; set; } = -1;
	private Vector2 StairOffset = new Vector2(0, 200);
	public override void Action()
	{
		if (CharaReachArea != null)
		{
			CharacterBody2D chara = CharaReachArea.GetParent() as CharacterBody2D;
			chara = CharaReachArea.GetParent() as CharacterBody2D;
			chara.Position = Position + StairOffset * Direction;
		}
	}
}

