using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Enemy : CharacterBase
{
	public override void Kill()
	{
		PoolManager.Recycle(this);
	}

	public override bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType)
	{
		return true;
	}
}
