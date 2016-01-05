using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class IntervalWeaponAttack : WeaponAttack
{
	protected virtual void Update()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (weapon != null && time.Time - lastAttackTime > 1f / GetAttackSpeed())
			Attack();
	}
}
