using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ForwardMotion : MotionBase
{
	protected override bool ShouldMove()
	{
		return true;
	}

	protected override Vector2 GetDirection()
	{
		return Transform.right;
	}

	protected override bool ShouldRotate()
	{
		return true;
	}

	protected override float GetAngle()
	{
		return Transform.eulerAngles.z;
	}
}
