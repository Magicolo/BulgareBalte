using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public struct DamageData
{
	public float Damage;
	public DamageSources Source;
	public DamageTypes Type;
	public Vector3 Position;

	public DamageData(float damage, DamageSources source, DamageTypes type, Vector3 position) : this(damage, source, type)
	{
		Damage = damage;
		Source = source;
		Type = type;
		Position = position;
	}

	public DamageData(float damage, DamageSources source, DamageTypes type)
	{
		Damage = damage;
		Source = source;
		Type = type;
		Position = Vector3.zero;
	}

	public override string ToString()
	{
		return string.Format("{0}({1}, {2}, {3}, {4})", GetType().Name, Damage, Source, Type, Position);
	}
}
