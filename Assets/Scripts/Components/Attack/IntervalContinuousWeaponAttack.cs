using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class IntervalContinuousWeaponAttack : WeaponAttack, IUpdateable
{
	public MinMax ContinuousDuration = new MinMax(0.75f, 1.5f);

	float nextAttackTime;
	float nextStopTime;
	bool startedAttacking;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (time.Time > nextStopTime)
			StopAttacking(time);
		else if (time.Time > nextAttackTime)
			HandleAttack();
	}

	private void HandleAttack()
	{
		Attack();

		if (!startedAttacking)
			Entity.SendMessage(EntityMessages.OnStartAttacking);
	}

	private void StopAttacking(TimeComponent time)
	{
		nextAttackTime = time.Time + 1f / AttackSpeed;
		nextStopTime = nextAttackTime + ContinuousDuration.GetRandom();
		Entity.SendMessage(EntityMessages.OnStopAttacking);
		startedAttacking = false;
	}
}
