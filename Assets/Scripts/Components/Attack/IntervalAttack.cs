using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class IntervalAttack : AttackBase
{
	protected override bool ShouldAttack()
	{
		return CachedTime.Time - lastAttackTime > 1f / (AttackSpeed * Weapon.AttackSpeedModifier);
	}
}
