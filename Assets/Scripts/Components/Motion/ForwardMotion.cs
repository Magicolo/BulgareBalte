using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ForwardMotion : MotionBase
{
	protected override Vector2 GetDirection()
	{
		return base.GetDirection() + Entity.Transform.right.ToVector2();
	}

	protected override float GetAngle()
	{
		return base.GetAngle() + Entity.Transform.eulerAngles.z;
	}

	protected override bool ShouldMove()
	{
		return true;
	}

	protected override bool ShouldRotate()
	{
		return true;
	}
}
