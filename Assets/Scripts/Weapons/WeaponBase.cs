﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class WeaponBase : PMonoBehaviour, ICopyable<WeaponBase>, IClonable<WeaponBase>
{
	public DamageTypes DamageType;
	[Min]
	public float AttackSpeedModifier = 3f;
	public float DamageModifier = 1f;

	public CharacterBase Owner { get; set; }

	public abstract void Fire();

	public void Copy(WeaponBase reference)
	{
		PDebug.LogMethod(reference);
	}

	public virtual WeaponBase Clone()
	{
		return Pools.BehaviourPool.CreateCopy(this);
	}
}