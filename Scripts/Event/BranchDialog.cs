using Godot;

[GlobalClass]
public partial class BranchDialog : Dialog
{
	[Export]
	public Dialog OKDialog { get; set; }
	[Export]
	public Dialog CancelDialog { get; set; }
}