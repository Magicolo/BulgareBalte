using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, ComponentCategory("General"), EntityRequires(typeof(TimeComponent))]
public class LifeTime : ComponentBase, IUpdateable
{
	public float Duration = 5f;

	float counter;
	public float Progress { get { return counter / Duration; } }

	public float UpdateRate { get { return 0f; } }

	public virtual void Update()
	{
		counter += Entity.GetComponent<TimeComponent>().DeltaTime;

		if (counter >= Duration)
			Entity.SendMessage(EntityMessages.OnDie);
	}
}
