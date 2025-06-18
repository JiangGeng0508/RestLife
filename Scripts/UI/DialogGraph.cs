using Godot;
using System;

[GlobalClass]
public partial class DialogGraph : GraphNode
{
	[Export]
	public Dialog dialog { get; set; }
	[Export]
	public string ExportPath { get; set; } = "res://Asset/Data/Dialogs/Export/";
	public Dialog ConfirmDialog;
	public Dialog CancelDialog;
	public TextEdit textEdit;
	public LineEdit title;
	public override void _Ready()
	{
		title = GetNode<LineEdit>("Title/DialogTitle");
		GetNode<Button>("Title/Button").Pressed += () =>
		{
			ExportDialog();
		};
		textEdit = GetNode<TextEdit>("EditContainer/DialogText");
		if (dialog != null)
		{
			title.Text = dialog.Title;
			textEdit.Text = dialog.DialogText;
		}
		SlotUpdated += (slot) =>
		{
			GD.Print("Slot updated" + slot);
		};
	}
	public string ExportDialog()
	{
		if (title == null || textEdit == null)
		{
			return "Fail";
		}
		dialog = new Dialog
		{
			Title = title.Text,
			DialogText = textEdit.Text,
			OKDialog = ConfirmDialog,
			CancelDialog = CancelDialog
		};
		ResourceSaver.Save(dialog, ExportPath + dialog.Title + ".tres");
		return ExportPath + dialog.Title + ".tres";
	}
}
