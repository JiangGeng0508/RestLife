using System;
using Godot;

public partial class AttributeManager : Node
{
	//任意属性变化时触发
	public Action AttributeChanged;
	// 状态值
	public Attribute Health = new(50f);
	public Attribute Energy = new(100f);
	public Attribute Hunger = new(100f);

	public override void _Ready()
	{
		//属性变化时连锁触发AttributeChanged
		Health.OnChange += () => { AttributeChanged?.Invoke(); };
		Energy.OnChange += () => { AttributeChanged?.Invoke(); };
		Hunger.OnChange += () => { AttributeChanged?.Invoke(); };
	}
	//应用buff
	public void ApplyBuff(Buff buff)
	{
		switch (buff.AttrName)
		{
			case "Health":
				Health.ApplyBonus(buff.Operator, buff.Value);
				break;
			case "Energy":
				Energy.ApplyBonus(buff.Operator, buff.Value);
				break;
			case "Hunger":
				Hunger.ApplyBonus(buff.Operator, buff.Value);
				break;
		}
		GetTree().CreateTimer(buff.Time, true, true).Timeout += () =>
		{
			switch (buff.AttrName)
			{
				case "Health":
					Health.BackBonus(buff.Operator, buff.Value);
					break;
				case "Energy":
					Energy.BackBonus(buff.Operator, buff.Value);
					break;
				case "Hunger":
					Hunger.BackBonus(buff.Operator, buff.Value);
					break;
			}
		};
	}
	public Attribute GetAttribute(string attrName) => attrName
	switch
	{
		"Health" => Health,
		"Energy" => Energy,
		"Hunger" => Hunger,
		_ => throw new System.Exception("Invalid attribute name"),
	};
}
