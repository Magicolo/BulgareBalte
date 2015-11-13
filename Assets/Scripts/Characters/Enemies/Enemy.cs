using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Copy]
public class Enemy : CharacterBase, ICopyable<Enemy>
{
	public override void Kill()
	{
		Pools.BehaviourPool.Recycle(this);

		if (!IsInPool)
			CachedGameObject.Destroy();
	}

	public override bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType)
	{
		return true;
	}

	public void Copy(Enemy reference)
	{
		base.Copy(reference);

	}
}
