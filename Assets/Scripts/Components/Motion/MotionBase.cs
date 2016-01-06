using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[ComponentCategory("Motion"), EntityRequires(typeof(TimeComponent))]
public abstract class MotionBase : ComponentBase, IStartable, IFixedUpdateable
{
	public float MoveSpeed = 10f;
	public float RotateSpeed = 3f;

	IList<MotionModifier> modifiers = new List<MotionModifier>();

	protected Rigidbody2D rigidbody;

	public void Start()
	{
		modifiers = Entity.GetComponents<MotionModifier>();
		rigidbody = Entity.GameObject.GetComponent<Rigidbody2D>();
	}

	public virtual void FixedUpdate()
	{
		UpdateMotion();
	}

	protected virtual void UpdateMotion()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (ShouldMove())
			rigidbody.AddForce(GetDirection() * GetMoveSpeed() * time.FixedDeltaTime * rigidbody.mass, ForceMode2D.Impulse);

		if (ShouldRotate())
			rigidbody.RotateTowards(GetAngle(), GetRotateSpeed() * time.FixedDeltaTime);
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

	protected abstract bool ShouldMove();
	protected abstract bool ShouldRotate();
}
