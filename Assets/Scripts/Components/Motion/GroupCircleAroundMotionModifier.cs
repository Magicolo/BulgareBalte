using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class GroupCircleAroundMotionModifier : MotionModifier
{

	public float CheckForTargetInterval = 1f;
	public EntityMatch TargetGroup;
	PEntity target;

	protected float nextPlayerCheckTime;


	protected virtual void UpdateTarget()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (target == null || time.Time > nextPlayerCheckTime)
		{
			target = GetTarget();
			nextPlayerCheckTime = time.Time + CheckForTargetInterval;
		}
		else if (!target.gameObject.activeSelf)
			target = null;
	}

	protected virtual PEntity GetTarget()
	{
		return (PEntity)EntityManager.GetEntityGroup(TargetGroup).Entities.GetClosest(Entity.Transform.position);
	}
}

