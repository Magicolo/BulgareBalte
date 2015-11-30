using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class IntervalContinuousWeaponAttack : WeaponAttack
{
	public float ContinuousDuration = 1f;

	float nextAttackTime;
	float nextStopTime;

	protected virtual void Update()
	{
		if (CachedTime.Time > nextStopTime)
		{
			nextAttackTime = CachedTime.Time + 1f / AttackSpeed;
			nextStopTime = nextAttackTime + ContinuousDuration;
		}
		else if (CachedTime.Time > nextAttackTime)
			Attack();
	}
}
