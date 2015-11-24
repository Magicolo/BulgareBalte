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

	public bool IsFiring { get; private set; }

	DamageData damage;

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
		CachedLineRenderer.enabled = IsFiring;
		Particles.GameObject.SetActive(IsFiring);

		if (!IsFiring)
			return;

		CachedRaycaster.Cast();
		UpdateLine();

		if (CachedRaycaster.Hits.Count > 0)
		{
			RaycastHit2D hit = CachedRaycaster.Hits.Last();
			IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();

			if (damageable != null)
			{
				if (damageable.CanBeDamagedBy(damage))
					damageable.Damage(damage);
			}
		}

		IsFiring = false;
	}

	void UpdateLine()
	{
		CachedLineRenderer.SetVertexCount(CachedRaycaster.BounceCount + 2);
		CachedLineRenderer.SetPosition(0, Transform.position);

		Vector3 endPosition = CachedRaycaster.EndPosition;
		endPosition.z = Transform.position.z;

		for (int i = 0; i < CachedRaycaster.BounceCount; i++)
		{
			RaycastHit2D hit = CachedRaycaster.Hits[i];
			CachedLineRenderer.SetPosition(i + 1, hit.point);
		}

		CachedLineRenderer.SetPosition(CachedRaycaster.BounceCount + 1, endPosition);
		Particles.Transform.position = endPosition;
		Particles.Transform.rotation = Quaternion.Euler(0f, 0f, CachedRaycaster.EndDirection.ToVector2().Angle() + 90f);
	}

	public override void Fire(DamageData damage)
	{
		damage.Damage *= DamageModifier;
		damage.Type = DamageType;
		this.damage = damage;
		IsFiring = true;
	}
}
