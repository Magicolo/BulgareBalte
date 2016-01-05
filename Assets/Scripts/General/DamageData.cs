using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Flags]
public enum DamageTypes
{
	Laser = 1 << 0,
	Plasma = 1 << 1,
	Fire = 1 << 2
}

[Serializable]
public struct DamageData
{
	public float Damage;

	[EntityGroups]
	public ByteFlag Sources;
	[EnumFlags]
	public DamageTypes Types;

	public DamageData(float damage, ByteFlag sources = default(ByteFlag), DamageTypes types = 0)
	{
		Damage = damage;
		Sources = sources;
		Types = types;
	}

	public override string ToString()
	{
		return string.Format("{0}({1}, {2}, {3})", GetType().Name, Damage, Sources, Types);
	}
}
