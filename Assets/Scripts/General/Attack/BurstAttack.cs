using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BurstAttack : AttackBase
{
	public float BurstInterval = 0.25f;
	public int BurstAmount = 3;

	int burstCounter;
	float nextAttackTime;

	protected override void Attack()
	{
		base.Attack();

		burstCounter++;

		if (burstCounter >= BurstAmount)
		{
			burstCounter = 0;
			nextAttackTime = CachedTime.Time + (1f / AttackSpeed);
		}
		else
			nextAttackTime = CachedTime.Time + BurstInterval;
	}

	protected override bool ShouldAttack()
	{
		return CachedTime.Time > nextAttackTime;
	}
}
