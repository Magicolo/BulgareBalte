﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class LaserDamager : DamagerBase
{
	public float DamageModifier = 1f;

	protected DamageData damage;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public LaserDamager()
	{
		cachedTime = new CachedValue<TimeComponent>(Entity.GameObject.GetComponent<TimeComponent>);
	}

	public override DamageData GetDamageData()
	{
		return damage;
	}

	public override void SetDamageData(DamageData damage)
	{
		damage.Damage *= DamageModifier * CachedTime.DeltaTime;
		damage.Types |= DamageTypes.Laser;
		this.damage = damage;
	}
}
