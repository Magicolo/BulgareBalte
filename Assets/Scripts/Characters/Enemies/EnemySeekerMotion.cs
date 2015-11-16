using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class EnemySeekerMotion : SeekerMotion
{
	public float CheckForTargetInterval = 1f;

	protected float nextPlayerCheckTime;

	public override void UpdateMotion()
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

	protected virtual Transform GetTarget()
	{
		Player player = Player.GetClosest(CachedTransform.position);

		return player == null ? null : player.CachedTransform;
	}
}
