using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class SinusMotionModifier : MotionModifier
{
	public MinMax Amplitude = new MinMax(25f, 100f);
	public MinMax Frequency = new MinMax(1f, 5f);
	public MinMax Offset = new MinMax(0f, 1000f);

	float randomAmplitude;
	float randomFrequency;
	float randomOffset;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public SinusMotionModifier()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	public override float GetAngleModifier()
	{
		return base.GetAngleModifier() + randomAmplitude * Mathf.Sin(CachedTime.Time * randomFrequency + randomOffset);
	}

	public override void OnCreate()
	{
		base.OnCreate();

		randomAmplitude = Amplitude.GetRandom();
		randomFrequency = Frequency.GetRandom();
		randomOffset = Offset.GetRandom();
	}
}
