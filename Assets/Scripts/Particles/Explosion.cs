using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Explosion : ParticleEffect
{
	[InitializeContent]
	public CircleZone DamageZone;
	public DamageData Damage;

	readonly Collider2D[] hits = new Collider2D[16];
	bool hasDamaged;

	protected virtual void Update()
	{
		UpdateDamage();
	}

	protected virtual void UpdateDamage()
	{
		if (hasDamaged)
			return;

		Physics2D.OverlapCircleNonAlloc(DamageZone.WorldCircle.Position, DamageZone.WorldCircle.Radius, hits);
		for (int i = 0; i < hits.Length; i++)
		{
			var hit = hits[i];

			if (hit == null)
				continue;

			var entity = hit.GetEntity();
			var damageable = entity == null ? null : entity.GetComponent<Damageable>();

			if (damageable != null)
				damageable.Damage(Damage);
		}

		hits.Clear();
		hasDamaged = true;
	}
}
