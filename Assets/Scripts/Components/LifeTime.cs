using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class LifeTime : ComponentBase
{
	public float Duration = 5f;

	float counter;

	protected virtual void Update()
	{
		UpdateLife();
	}

	protected virtual void UpdateLife()
	{
		counter += Entity.GetComponent<TimeComponent>().DeltaTime;

		if (counter >= Duration)
			Entity.SendMessage(EntityMessages.OnDie);
	}
}
