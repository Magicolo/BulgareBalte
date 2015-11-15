using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public abstract class SeekerEnemy : Enemy
{
	public float StopDistance = 1f;
	public float CheckInterval = 1f;

	protected Transform target;
	protected float nextPlayerCheckTime;

	readonly CachedValue<Rigidbody2D> cachedRigidbody;
	public Rigidbody2D CachedRigidbody { get { return cachedRigidbody; } }

	protected SeekerEnemy()
	{
		cachedRigidbody = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	protected virtual void Update()
	{
		UpdateTarget();
	}

	protected virtual void FixedUpdate()
	{
		UpdateMotion();
	}

	protected virtual void UpdateTarget()
	{
		if (target == null || CachedTime.Time > nextPlayerCheckTime)
		{
			target = GetTarget();
			nextPlayerCheckTime = CachedTime.Time + CheckInterval;
		}
	}

	protected virtual void UpdateMotion()
	{
		if (target == null)
			return;

		CachedRigidbody.AddForce(GetDirection() * currentStats.MovementSpeed * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);
		CachedRigidbody.RotateTowards(GetAngle(), CachedTime.FixedDeltaTime * CurrentStats.RotationSpeed);
	}

	protected virtual Transform GetTarget()
	{
		Player player = Player.GetClosest(CachedTransform.position);

		return player == null ? null : player.CachedTransform;
	}

	protected virtual Vector2 GetDirection()
	{
		if (Vector2.Distance(CachedTransform.position, target.position) > StopDistance)
			return CachedTransform.right;
		else
			return -CachedTransform.right;
	}

	protected virtual float GetAngle()
	{
		Vector2 direction = (target.position - CachedTransform.position).normalized;

		return direction.Angle();
	}
}
