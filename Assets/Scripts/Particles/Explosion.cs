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
	public DamageTypes DamageType;

	DamageData damage;
	bool hasCausedDamage;

	public virtual void Initialize(DamageData damage)
	{
		damage.Type = DamageType;
		this.damage = damage;
	}

	protected override void Update()
	{
		base.Update();

		UpdateDamage();
	}

	void UpdateDamage()
	{
		if (hasCausedDamage)
			return;

		Collider2D[] hits = Physics2D.OverlapCircleAll(DamageZone.WorldCircle.Position, DamageZone.WorldCircle.Radius);

		for (int i = 0; i < hits.Length; i++)
		{
			IDamageable damageable = hits[i].GetComponentInParent<IDamageable>();

			if (damageable != null)
				damageable.Damage(damage);
		}

		hasCausedDamage = true;
	}
}
