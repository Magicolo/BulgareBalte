using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Copy]
public class WeaponStats : ICopyable<WeaponStats>
{
	[Min]
	public float Damage;
	[Min]
	public float AttackSpeed;
	public DamageTypes DamageType;

	public void Copy(WeaponStats reference)
	{
		throw new NotImplementedException();
	}
}
