using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class SinusMotionModifier : MotionModifier, IStartable
{
	public MinMax Amplitude = new MinMax(25f, 100f);
	public MinMax Frequency = new MinMax(1f, 5f);
	public MinMax Offset = new MinMax(0f, 1000f);

	float randomAmplitude;
	float randomFrequency;
	float randomOffset;

	public void Start()
	{
		randomAmplitude = Amplitude.GetRandom();
		randomFrequency = Frequency.GetRandom();
		randomOffset = Offset.GetRandom();
	}

	public override float GetAngleModifier()
	{
		var time = Entity.GetComponent<TimeComponent>();

		return base.GetAngleModifier() + randomAmplitude * Mathf.Sin(time.Time * randomFrequency + randomOffset);
	}
}
