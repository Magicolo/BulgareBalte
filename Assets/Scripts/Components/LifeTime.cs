using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class LifeTime : PComponent
{
	public float Duration = 5f;

	float counter;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	public LifeTime()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	protected virtual void Update()
	{
		UpdateLife();
	}

	protected virtual void UpdateLife()
	{
		counter += CachedTime.DeltaTime;

		if (counter >= Duration)
			Entity.SendMessage("OnDie");
	}
}
