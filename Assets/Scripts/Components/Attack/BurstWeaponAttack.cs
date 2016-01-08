using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class BurstWeaponAttack : WeaponAttack, IUpdateable
{
	public float BurstInterval = 0.25f;
	public int BurstAmount = 3;

	int burstCounter;
	float nextAttackTime;

	public float UpdateRate
	{
		get { return 0f; }
	}

	public virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (weapon != null && time.Time > nextAttackTime)
			Attack();
	}

	public override void Attack()
	{
		base.Attack();

		var time = Entity.GetComponent<TimeComponent>();

		if (burstCounter == 0)
			Entity.SendMessage(EntityMessages.OnStartAttacking);

		burstCounter++;

		if (burstCounter >= BurstAmount)
		{
			burstCounter = 0;
			nextAttackTime = time.Time + 1f / GetAttackSpeed();
			Entity.SendMessage(EntityMessages.OnStopAttacking);
		}
		else
			nextAttackTime = time.Time + BurstInterval;
	}
}
