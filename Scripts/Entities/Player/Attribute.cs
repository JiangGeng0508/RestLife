using System;
using Godot;

public struct Attribute
{
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
		}
	}
	public Attribute(float maxValue)
	{
		MaxValue = maxValue;
		Value = MaxValue;
	}
}