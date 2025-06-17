using Godot;
using System;

[GlobalClass]
public partial class DialogGraph : GraphNode
{
	[Export]
	public Dialog dialog { get; set; }
	public Dialog ConfirmDialog;
	public Dialog CancelDialog;
	public TextEdit textEdit;
	public LineEdit title;
	public override void _Ready()
	{
		title = GetNode<LineEdit>("Title/DialogTitle");
		GetNode<Button>("Title/Button").Pressed += () =>
		{
			ExportDialog(dialog);
		};
		textEdit = GetNode<TextEdit>("EditContainer/DialogText");
		if (dialog != null)
		{
			title.Text = dialog.Title;
			textEdit.Text = dialog.DialogText;
		}
	}
	public void ExportDialog(Dialog dialog)
	{
		if (title == null || textEdit == null)
		{
			return;
		}
		GD.Print("Exported Dialog: " + dialog.Title);
		ResourceSaver.Save(dialog, "res://Asset/Data/Dialogs/Export/" + dialog.Title + ".tres");
	}

}
