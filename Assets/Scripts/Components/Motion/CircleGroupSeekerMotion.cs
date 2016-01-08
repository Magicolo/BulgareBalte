using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable]
public class CircleGroupSeekerMotion : GroupSeekerMotion
{
	protected override Vector2 GetDirection()
	{
		var direction = base.GetDirection();

		if (!base.ShouldMove())
		{
			var targetToMe = (Entity.Transform.position - Target.transform.position).ToPolar();
			targetToMe = targetToMe.SetValues(targetToMe.y + 15, Axes.Y);

			var targetPosition = Target.transform.position + targetToMe.ToCartesian();

			var movementVector = Entity.Transform.position - targetPosition;

			direction = direction + movementVector.ToVector2();
		}

		return direction;
	}

	protected override bool ShouldMove()
	{
		return true;
	}
}

