﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class GroupSeekerMotion : SeekerMotion
{
	public float CheckForTargetInterval = 1f;
	public EntityGroup.Groups TargetGroup;

	protected float nextPlayerCheckTime;

	protected override void UpdateMotion()
	{
		UpdateTarget();
		base.UpdateMotion();
	}

	protected virtual void UpdateTarget()
	{
		if (Target == null || CachedTime.Time > nextPlayerCheckTime)
		{
			Target = GetTarget();
			nextPlayerCheckTime = CachedTime.Time + CheckForTargetInterval;
		}
		else if (!Target.gameObject.activeSelf)
			Target = null;
	}

	protected virtual PEntity GetTarget()
	{
		return EntityGroup.GetClosestEntity(TargetGroup, Transform.position);
	}
}