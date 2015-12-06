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
	[SerializeField, PropertyField(typeof(EnumFlagsAttribute), typeof(EntityGroups))]
	ByteFlag sources;
	public ByteFlag<EntityGroups> Sources
	{
		get { return sources; }
		set { sources = value; }
	}
	[EnumFlags]
	public DamageTypes Types;

	public DamageData(float damage, ByteFlag<EntityGroups> source = default(ByteFlag<EntityGroups>), DamageTypes type = 0)
	{
		Damage = damage;
		sources = source;
		Types = type;
	}

	public override string ToString()
	{
		return string.Format("{0}({1}, {2}, {3})", GetType().Name, Damage, Sources, Types);
	}
}
