using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class MotionModifier : ComponentBase
{
	public float MoveSpeedModifier = 1f;
	public float RotateSpeedModifier = 1f;

	public virtual Vector2 GetDirectionModifier()
	{
		return Vector2.one;
	}

	public virtual float GetAngleModifier()
	{
		return 0f;
	}
}
