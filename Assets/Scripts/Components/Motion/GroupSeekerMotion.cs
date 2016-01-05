using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class GroupSeekerMotion : SeekerMotion
{
	public float CheckForTargetInterval = 1f;
	public EntityMatch TargetGroup;

	protected float nextPlayerCheckTime;

	protected override void UpdateMotion()
	{
		UpdateTarget();
		base.UpdateMotion();
	}

	protected virtual void UpdateTarget()
	{
		var time = Entity.GetComponent<TimeComponent>();

		if (Target == null || time.Time > nextPlayerCheckTime)
		{
			Target = GetTarget();
			nextPlayerCheckTime = time.Time + CheckForTargetInterval;
		}
		else if (!Target.gameObject.activeSelf)
			Target = null;
	}

	protected virtual PEntity GetTarget()
	{
		return (PEntity)EntityManager.GetEntityGroup(TargetGroup).Entities.GetClosest(Entity.Transform.position);
	}
}
