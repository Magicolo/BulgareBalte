using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class BurstWeaponAttack : WeaponAttack
{
	public float BurstInterval = 0.25f;
	public int BurstAmount = 3;

	int burstCounter;
	float nextAttackTime;

	protected virtual void Update()
	{
		if (weapon != null && CachedTime.Time > nextAttackTime)
			Attack();
	}

	public override void Attack()
	{
		base.Attack();

		burstCounter++;

		if (burstCounter >= BurstAmount)
		{
			burstCounter = 0;
			nextAttackTime = CachedTime.Time + 1f / GetAttackSpeed();
		}
		else
			nextAttackTime = CachedTime.Time + BurstInterval;
	}
}
