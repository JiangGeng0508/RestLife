using Godot;

public partial class Buff : Resource
{
	//Dec Attr Value Time
	public string AttrName { get; set; } = "Hunger";
	public AttrOperator Operator { get; set; } = AttrOperator.Add;
	public float Value { get; set; } = 0;
	public float Time { get; set; } = 0;// minutes(actually seconds)
	public Buff() { }
	//通过属性名 运算符 数值 时间 创建Buff
	//属性名：Health Hunger Energy
	//运算符：Add Multiply MaxAdd MaxMultiply
	public Buff(AttrOperator op, string attr, float value, float time)
	{
		AttrName = attr;
		Operator = op;
		Value = value;
		Time = time;
	}
}