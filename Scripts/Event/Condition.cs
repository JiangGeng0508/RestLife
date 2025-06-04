using System;
using Godot;

public enum ConditionType
{
	Time,
	Attribute,
}
public enum ConditionOperator
{
	Equal,
	GreaterThan,
	LessThan,
}
public delegate void ConditionMeet();
public class Condition
{
	public ConditionType Type;
	float Source;

	public Delegate Meet { get; set; }

	ConditionOperator Operator;
	public float Value;
	public bool Check()
	{
		GD.Print($"Checking Condition {Source} {Value}");
		switch (Operator)
		{
			case ConditionOperator.Equal:
				Meet?.DynamicInvoke();
				return Source == Value;
			case ConditionOperator.GreaterThan:
				return Source > Value;
			case ConditionOperator.LessThan:
				return Source < Value;
			default:
				GD.Print("Invalid Operator Type");
				return false;
		}
		
	}
	public void CheckHandler(int PassInValue)
	{
		GD.Print($"CheckHandler Condition {Source} {PassInValue}");
		Value = PassInValue;
	}
	public Condition() { }
	public Condition(ConditionType type, string resolution, ConditionOperator op, float value)
	{
		Type = type;
		Operator = op;
		Source = value;
		switch (type)
		{
			case ConditionType.Time:
				switch (resolution)
				{
					case "Day":
						Global.GameWorldTime.OnDayChange += CheckHandler;
						break;
					case "Hour":
						break;
					case "Minute":
						break;
					case "Second":
						break;
					default:
						GD.Print("Invalid Condition Resolution");
						break;
				}
				break;
			case ConditionType.Attribute:
				switch (resolution)
				{
					case "Health":
						break;
					case "Energy":
						break;
					case "Hunger":
						break;
					default:
						GD.Print("Invalid Condition Resolution");
						break;
				}
				break;
			default:
				GD.Print("Invalid Condition Type");
				break;
		}
	}
}