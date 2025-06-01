using Godot;
using System;

public partial class DialogueLayer : CanvasLayer
{
	public override void _Ready()
	{
		base._Ready();
		GetNode<Label>("DialogueLabel").Text = "None";
	}

	public void ShowDialogue(string dialogue)
	{
		GetNode<Label>("DialogueLabel").Text = dialogue;
	}
}
