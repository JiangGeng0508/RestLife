using Godot;
using System;

public partial class DialogueLayer : CanvasLayer
{
	Label DialogueLabel;
	Timer DialogueTimer;
	Label SayLabel;
	Timer SayTimer;
	ConfirmationDialog DialoguePop;
	RichTextLabel DialogueTextLabel;
	public override void _Ready()
	{
		DialogueLabel = GetNode<Label>("DialogueLabel");
		DialogueTimer = GetNode<Timer>("DialogueLabel/DialogueTimer");
		DialogueTimer.Timeout += DialogueLabel.Hide;
		SayLabel = GetNode<Label>("SayLabel");
		SayTimer = GetNode<Timer>("SayLabel/SayTimer");
		SayTimer.Timeout += SayLabel.Hide;
		DialoguePop = GetNode<ConfirmationDialog>("DialoguePop");
		DialogueTextLabel = GetNode<RichTextLabel>("DialoguePop/DialogueTextLabel");
	}

	public void ShowDialogue(string dialogue, float duration)
	{
		DialogueLabel.Text = dialogue;
		DialogueLabel.Show();
		DialogueTimer.Start(duration);
	}
	public void ShowSay(string say, float duration)
	{
		SayLabel.Text = say;
		SayLabel.Show();
		SayTimer.Start(duration);
	}

	public void ShowPopup(string message)
	{
		DialoguePop.Show();
		DialoguePop.PopupCentered();
		DialogueTextLabel.Text = message;
	}
}
