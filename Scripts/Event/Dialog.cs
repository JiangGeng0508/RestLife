using Godot;

[GlobalClass]
public partial class Dialog : Resource
{
	[Export]
	public string DialogText { get; set; }

	public virtual void OnOK()
	{
		GD.Print("OK");
	}
	public virtual void OnCancel()
	{
		GD.Print("Cancel");
	}
}