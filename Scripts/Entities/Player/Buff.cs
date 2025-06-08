using Godot;

public partial class Buff : Resource
{
	//Dec Attr Value Time
	public string AttrName { get; set; } = "Hunger";
	public AttrOperator Operator { get; set; } = AttrOperator.Add;
	public float Value { get; set; } = 0;
	public float Time { get; set; } = 0;// minutes(actually seconds)
	public Buff() { }
	public Buff(AttrOperator op, string attr, float value, float time)
	{
		AttrName = attr;
		Operator = op;
		Value = value;
		Time = time;
	}
}