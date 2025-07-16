using System;
using Godot;

public enum AttrOperator
{
	Add,
	Multiply,
	MaxAdd,
	MaxMultiply
}
public struct Attribute
{
	public Action OnChange;
	public static float MaxValue;
	private float _value;
	public float Value
	{
		get
		{
			return _value;
		}
		set
		{
			if (_value == -1) return;
			if (value <= 0)
			{
				_value = -1;
			}
			else if (value > MaxValue)
			{
				_value = MaxValue;
			}
			else
			{
				_value = value;
			}
			OnChange?.Invoke();
		}
	}
	//应用buff
	public void ApplyBonus(AttrOperator op, float value)
	{
		switch (op)
		{
			case AttrOperator.Add:
				Value += value;
				break;
			case AttrOperator.Multiply:
				Value *= value;
				break;
			case AttrOperator.MaxAdd:
				MaxValue += value;
				break;
			case AttrOperator.MaxMultiply:
				MaxValue *= value;
				break;
		}
	}
	//结束buff
	public void BackBonus(AttrOperator op, float value)
	{
		switch (op)
		{
			case AttrOperator.Add:
				Value -= value;
				break;
			case AttrOperator.Multiply:
				Value /= value;
				break;
			case AttrOperator.MaxAdd:
				MaxValue -= value;
				break;
			case AttrOperator.MaxMultiply:
				MaxValue /= value;
				break;
		}
	}
	//主构造函数，从最大值初始化
	public Attribute(float maxValue)
	{
		MaxValue = maxValue;
		Value = MaxValue;
	}
}