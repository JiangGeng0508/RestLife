using Godot;
using System;

public partial class RideableItem : InteractableItem
{
	[Export] public Vector2 riderOffset { get; set; } = new Vector2(0, -20);
	private Vector2 _lastPosition;
	Character rider;
	bool toggle = false;

	public override void Action()
	{
		toggle = !toggle;
		if (CharaReachArea != null && toggle)
		{
			rider = CharaReachArea.GetParent() as Character;
			rider.Ride(this);
		}
		else if(rider != null && !toggle)
		{
			rider.StopRiding();
			rider = null;
		}
	}
}
