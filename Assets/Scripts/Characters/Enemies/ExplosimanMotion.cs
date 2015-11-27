using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ExplosimanMotion : GroupSeekerMotion
{
	float randomAmplitude;
	float randomFrequency;
	float randomOffset;

	protected override float GetAngle()
	{
		return base.GetAngle() + randomAmplitude * Mathf.Sin(CachedTime.Time * randomFrequency + randomOffset);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		randomAmplitude = PRandom.Range(25f, 100f);
		randomFrequency = PRandom.Range(1f, 5f);
		randomOffset = PRandom.Range(0f, 1000f);
	}
}
