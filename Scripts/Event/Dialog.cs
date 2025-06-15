using Godot;

[GlobalClass]
public partial class Dialog : Resource
{
	[Export]
	public string DialogText { get; set; }
	[Export]
	public Dialog OKDialog { get; set; }
	[Export]
	public Dialog CancelDialog { get; set; }

	public virtual void Init()
	{
		
	}
	public virtual void OnOK()
	{
		GD.Print("OK");
	}
	public virtual void OnCancel()
	{
		GD.Print("Cancel");
	}
}