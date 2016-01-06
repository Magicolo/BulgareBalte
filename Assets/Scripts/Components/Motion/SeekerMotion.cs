using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SeekerMotion : MotionBase
{
	public PEntity Target;
	public float Threshold = 0.5f;
	public float RotationThreshold = 0.5f;
	public float StopDistance = 1f;

	public bool LookingAtTarget { get { return (Target != null) && Math.Abs(GetAngle() - Entity.Transform.rotation.eulerAngles.z) < RotationThreshold; } }

	protected override Vector2 GetDirection()
	{
		Vector2 direction = base.GetDirection();

		if (Vector2.Distance(Entity.Transform.position, Target.CachedTransform.position) > StopDistance)
			direction += Entity.Transform.right.ToVector2();
		else
			direction -= Entity.Transform.right.ToVector2();

		return direction;
	}

	protected override float GetAngle()
	{
		return base.GetAngle() + (Target.CachedTransform.position - Entity.Transform.position).ToVector2().Angle();
	}

	protected override bool ShouldMove()
	{
		return Target != null && Mathf.Abs(Vector2.Distance(Entity.Transform.position, Target.CachedTransform.position) - StopDistance) > Threshold;
	}

	protected override bool ShouldRotate()
	{
		return Target != null;
	}
}
