using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class LaserDamager : DamagerBase
{
	public float DamageModifier = 1f;

	protected DamageData damage;

	readonly CachedValue<TimeComponent> cachedTime;

	public override DamageData GetDamageData()
	{
		return damage;
	}

	public override void SetDamageData(DamageData damage)
	{
		var time = Entity.GetComponent<TimeComponent>();

		damage.Damage *= DamageModifier * time.DeltaTime;
		damage.Types |= DamageTypes.Laser;
		this.damage = damage;
	}
}
