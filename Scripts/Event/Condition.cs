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
	GreaterOrEqual,
	LessOrEqual,
	NotEqual,
}
public class Condition
{
	public ConditionType Type;
	float Source;

	public Delegate Meet { get; set; }

	ConditionOperator Operator;
	public float CheckValue;
	public void Check()
	{
		switch (Operator)
		{
			case ConditionOperator.Equal:
				if (Source == CheckValue)
					Meet?.DynamicInvoke();
				break;
			case ConditionOperator.GreaterThan:
				if (Source < CheckValue)
					Meet?.DynamicInvoke();
				break;
			case ConditionOperator.LessThan:
				if (Source > CheckValue)
					Meet?.DynamicInvoke();
				break;
			case ConditionOperator.GreaterOrEqual:
				if (Source <= CheckValue)
					Meet?.DynamicInvoke();
				break;
			case ConditionOperator.LessOrEqual:
				if (Source >= CheckValue)
					Meet?.DynamicInvoke();
				break;
			case ConditionOperator.NotEqual:
				if (Source != CheckValue)
					Meet?.DynamicInvoke();
				break;
			default:
				GD.Print("Invalid Condition Operator");
				break;
		}
		
	}
	public void CheckHandler(int PassInValue)
	{
		CheckValue = PassInValue;
		Check();
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
