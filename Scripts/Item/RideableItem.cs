using Godot;
using System;

public partial class RideableItem : InteractableItem
{
	[Export] public Vector2 riderOffset { get; set; } = new Vector2(0, -20);
	private Vector2 _lastPosition;
	private bool isRiding = false;
	CharacterBody2D rider;
	public override void Action()
	{
		if (CharaReachArea != null)
		{
			rider = CharaReachArea.GetParent() as CharacterBody2D;
			_lastPosition = rider.Position;
			rider.Position = Position + riderOffset;
			isRiding = true;
			rider.ProcessMode = ProcessModeEnum.Disabled;
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (isRiding && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Right && rider != null)
			{
				rider.ProcessMode = ProcessModeEnum.Pausable;
				rider.Position = _lastPosition;
				isRiding = false;
				rider = null;
			}
			if (Interactable && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				Action();
			}
		}
	}
}
