using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class IntervalContinuousWeaponAttack : WeaponAttack
{
	public float ContinuousDuration = 1f;

	float nextAttackTime;
	float nextStopTime;

	protected virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (time.Time > nextStopTime)
		{
			nextAttackTime = time.Time + 1f / AttackSpeed;
			nextStopTime = nextAttackTime + ContinuousDuration;
		}
		else if (time.Time > nextAttackTime)
			Attack();
	}
}
