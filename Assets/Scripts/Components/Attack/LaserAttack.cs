using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(LaserRaycaster2D), typeof(LineRenderer))]
public class LaserAttack : AttackBase
{
	public Color ActiveColor = Color.red;
	public Color InactiveColor = new Color(0.5f, 0f, 0f, 0.25f);
	public ParticleEffect Particles;

	bool isAttacking;

	readonly CachedValue<LaserRaycaster2D> cachedRaycaster;
	public LaserRaycaster2D CachedRaycaster { get { return cachedRaycaster; } }

	readonly CachedValue<LineRenderer> cachedLineRenderer;
	public LineRenderer CachedLineRenderer { get { return cachedLineRenderer; } }

	public LaserAttack()
	{
		cachedRaycaster = new CachedValue<LaserRaycaster2D>(GetComponent<LaserRaycaster2D>);
		cachedLineRenderer = new CachedValue<LineRenderer>(GetComponent<LineRenderer>);
	}

	protected virtual void Update()
	{
		UpdateLine();
		Particles.CachedGameObject.SetActive(isAttacking);
		isAttacking = false;
	}

	public override void Attack()
	{
		isAttacking = true;

		Particles.CachedGameObject.SetActive(true);

		if (CachedRaycaster.Hits.Count > 0)
		{
			var hit = CachedRaycaster.Hits.Last();
			Damager.Damage(hit.collider.GetComponentInParent<IDamageable>());
		}
	}

	void UpdateLine()
	{
		CachedRaycaster.Cast();
		CachedLineRenderer.SetVertexCount(CachedRaycaster.BounceCount + 2);
		CachedLineRenderer.SetPosition(0, CachedTransform.position);
		CachedLineRenderer.material.color = isAttacking ? ActiveColor : InactiveColor;

		var endPosition = CachedRaycaster.EndPosition;
		endPosition.z = CachedTransform.position.z;

		for (int i = 0; i < CachedRaycaster.BounceCount; i++)
		{
			var hit = CachedRaycaster.Hits[i];
			CachedLineRenderer.SetPosition(i + 1, hit.point);
		}

		CachedLineRenderer.SetPosition(CachedRaycaster.BounceCount + 1, endPosition);
		Particles.CachedTransform.position = endPosition;
		Particles.CachedTransform.rotation = Quaternion.Euler(0f, 0f, CachedRaycaster.EndDirection.ToVector2().Angle() + 90f);
	}
}
