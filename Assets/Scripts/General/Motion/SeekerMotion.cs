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
		return Target != null && Mathf.Abs(Vector2.Distance(Transform.position, Target.position) - StopDistance) > Threshold;
	}

	protected override Vector2 GetDirection()
	{
		if (Vector2.Distance(Transform.position, Target.position) > StopDistance)
			return Transform.right;
		else
			return -Transform.right;
	}

	protected override bool ShouldRotate()
	{
		return Target != null;
	}

	protected override float GetAngle()
	{
		Vector2 direction = (Target.position - Transform.position).normalized;

		return direction.Angle();
	}
}
