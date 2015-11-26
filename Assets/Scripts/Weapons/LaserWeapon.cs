using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(LaserRaycaster2D), typeof(LineRenderer))]
public class LaserWeapon : WeaponBase
{
	[DoNotInitialize]
	public ParticleEffect Particles;

	public bool IsAttacking { get; private set; }

	readonly CachedValue<LaserRaycaster2D> cachedRaycaster;
	public LaserRaycaster2D CachedRaycaster { get { return cachedRaycaster; } }

	readonly CachedValue<LineRenderer> cachedLineRenderer;
	public LineRenderer CachedLineRenderer { get { return cachedLineRenderer; } }

	public LaserWeapon()
	{
		cachedRaycaster = new CachedValue<LaserRaycaster2D>(GetComponent<LaserRaycaster2D>);
		cachedLineRenderer = new CachedValue<LineRenderer>(GetComponent<LineRenderer>);
	}

	void Update()
	{
		UpdateLaser();
	}

	void UpdateLaser()
	{
		CachedLineRenderer.enabled = IsAttacking;
		Particles.GameObject.SetActive(IsAttacking);

		if (!IsAttacking)
			return;

		CachedRaycaster.Cast();
		UpdateLine();

		if (CachedRaycaster.Hits.Count > 0)
		{
			var hit = CachedRaycaster.Hits.Last();
			var damageable = hit.collider.GetComponentInParent<IDamageable>();

			if (damageable != null)
			{
				if (damageable.CanBeDamagedBy(damage))
					damageable.Damage(damage);
			}
		}

		IsAttacking = false;
	}

	void UpdateLine()
	{
		CachedLineRenderer.SetVertexCount(CachedRaycaster.BounceCount + 2);
		CachedLineRenderer.SetPosition(0, Transform.position);

		var endPosition = CachedRaycaster.EndPosition;
		endPosition.z = Transform.position.z;

		for (int i = 0; i < CachedRaycaster.BounceCount; i++)
		{
			var hit = CachedRaycaster.Hits[i];
			CachedLineRenderer.SetPosition(i + 1, hit.point);
		}

		CachedLineRenderer.SetPosition(CachedRaycaster.BounceCount + 1, endPosition);
		Particles.Transform.position = endPosition;
		Particles.Transform.rotation = Quaternion.Euler(0f, 0f, CachedRaycaster.EndDirection.ToVector2().Angle() + 90f);
	}

	public override void Attack(DamageData damage)
	{
		base.Attack(damage);

		IsAttacking = true;
	}
}
