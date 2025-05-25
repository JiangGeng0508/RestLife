using Godot;
using System;

public partial class RideableItem : InteractableItem
{
	[Export] public Vector2 riderOffset { get; set; } = new Vector2(0, 0);

	public override void Action()
	{
		if (CharaReachArea != null)
		{
			CharacterBody2D chara = CharaReachArea.GetParent() as CharacterBody2D;
			chara.Position = Position + riderOffset;
			chara.ProcessMode = ProcessModeEnum.Disabled;
		}
	}
}
