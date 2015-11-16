using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class StraightMotion : MotionBase
{
	protected override bool ShouldMove()
	{
		return true;
	}

	protected override Vector2 GetDirection()
	{
		return CachedTransform.right;
	}

	protected override bool ShouldRotate()
	{
		return true;
	}

	protected override float GetAngle()
	{
		return CachedTransform.eulerAngles.z;
	}
}
