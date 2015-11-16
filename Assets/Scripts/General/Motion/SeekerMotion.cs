using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SeekerMotion : MotionBase
{
	public Transform Target;
	public float Threshold = 0.5f;
	public float StopDistance = 1f;

	protected override bool ShouldMove()
	{
		return Target != null && Mathf.Abs(Vector2.Distance(CachedTransform.position, Target.position) - StopDistance) > Threshold;
	}

	protected override Vector2 GetDirection()
	{
		if (Vector2.Distance(CachedTransform.position, Target.position) > StopDistance)
			return CachedTransform.right;
		else
			return -CachedTransform.right;
	}

	protected override bool ShouldRotate()
	{
		return Target != null;
	}

	protected override float GetAngle()
	{
		Vector2 direction = (Target.position - CachedTransform.position).normalized;

		return direction.Angle();
	}
}
