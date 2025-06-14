using Godot;
using System;

public partial class Bed : InteractableItem
{
	public ConfirmationDialog SleepConfirmation;
	public override void Init()
	{
		SleepConfirmation = GetNode<ConfirmationDialog>("SleepConfirm");
		SleepConfirmation.Confirmed += () =>
		{
			Global.Player.Character.State = CharacterState.Sleeping;
		};
	}
	public override void Action()
	{
		SleepConfirmation.Show();
	}
}
