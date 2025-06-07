using Godot;
using System;

public partial class StairDown : InteractableItem
{
	public override void Action()
	{
		if (CharaReachArea != null)
		{
			CharacterBody2D chara = CharaReachArea.GetParent() as CharacterBody2D;
			chara = CharaReachArea.GetParent() as CharacterBody2D;
			chara.Position = Position + new Vector2(0, 200);
		}
	}
}

