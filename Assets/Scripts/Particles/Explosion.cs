﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(CircleZone))]
public class Explosion : ParticleEffect, ICopyable<Explosion>
{
	public float Damage = 5f;
	public DamageSources DamageSource;
	[DoNotCopy]
	public CircleZone DamageZone;

	bool hasCausedDamage;

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
				damageable.Damage(new DamageData(Damage, DamageSource, DamageTypes.Explosion, CachedTransform.position));
		}

		hasCausedDamage = true;
	}

	public void Copy(Explosion reference)
	{
		base.Copy(reference);

		Damage = reference.Damage;
		DamageSource = reference.DamageSource;
		hasCausedDamage = reference.hasCausedDamage;
	}
}