using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent), typeof(Rigidbody2D))]
public abstract class MotionBase : PComponent
{
	public float MoveSpeed = 10f;
	public float RotateSpeed = 3f;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	readonly CachedValue<Rigidbody2D> cachedRigidbody;
	public Rigidbody2D CachedRigidbody { get { return cachedRigidbody; } }

	protected MotionBase()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
		cachedRigidbody = new CachedValue<Rigidbody2D>(GetComponent<Rigidbody2D>);
	}

	protected virtual void FixedUpdate()
	{
		UpdateMotion();
	}

	protected virtual void UpdateMotion()
	{
		if (ShouldMove())
			CachedRigidbody.AddForce(GetDirection() * MoveSpeed * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);

		if (ShouldRotate())
			CachedRigidbody.RotateTowards(GetAngle(), RotateSpeed * CachedTime.FixedDeltaTime);
	}

	protected abstract bool ShouldMove();
	protected abstract bool ShouldRotate();
	protected abstract Vector2 GetDirection();
	protected abstract float GetAngle();
}
