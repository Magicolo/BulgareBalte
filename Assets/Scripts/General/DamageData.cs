using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class DamageData
{
	public float Damage;
	public DamageSources Source;
	public DamageTypes Type;

	public void Initialize(float damage, DamageSources source = DamageSources.None, DamageTypes type = DamageTypes.None)
	{
		Damage = damage;
		Source = source;
		Type = type;
	}

	public override string ToString()
	{
		return string.Format("{0}({1}, {2}, {3})", GetType().Name, Damage, Source, Type);
	}
}
