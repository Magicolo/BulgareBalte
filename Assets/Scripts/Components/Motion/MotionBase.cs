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

	List<MotionModifier> modifiers = new List<MotionModifier>();

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
			CachedRigidbody.AddForce(GetDirection() * GetMoveSpeed() * CachedTime.FixedDeltaTime, ForceMode2D.Impulse);

		if (ShouldRotate())
			CachedRigidbody.RotateTowards(GetAngle(), GetRotateSpeed() * CachedTime.FixedDeltaTime);
	}

	protected virtual float GetMoveSpeed()
	{
		float speed = MoveSpeed;

		for (int i = 0; i < modifiers.Count; i++)
			speed *= modifiers[i].MoveSpeedModifier;

		return speed;
	}

	protected virtual float GetRotateSpeed()
	{
		float speed = RotateSpeed;

		for (int i = 0; i < modifiers.Count; i++)
			speed *= modifiers[i].RotateSpeedModifier;

		return speed;
	}

	protected virtual Vector2 GetDirection()
	{
		Vector2 direction = Vector2.zero;
		for (int i = 0; i < modifiers.Count; i++)
			direction = direction.Mult(modifiers[i].GetDirectionModifier());

		return direction;

	}

	protected virtual float GetAngle()
	{
		float angle = 0f;
		for (int i = 0; i < modifiers.Count; i++)
			angle += modifiers[i].GetAngleModifier();

		return angle;
	}

	public override void OnCreate()
	{
		base.OnCreate();

		modifiers = Entity.GetComponents<MotionModifier>();
	}

	protected abstract bool ShouldMove();
	protected abstract bool ShouldRotate();
}
