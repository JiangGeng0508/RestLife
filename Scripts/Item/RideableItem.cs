using Godot;
using System;

public partial class RideableItem : InteractableItem
{
	[Export] public Vector2 riderOffset { get; set; } = new Vector2(0, -20);
	private Vector2 _lastPosition;
	public Character rider;

	public override void Action()
	{
		if (CharaReachArea != null && rider == null)
		{
			rider = CharaReachArea.GetParent() as Character;
			rider.Ride(this);
		}
	}
}
