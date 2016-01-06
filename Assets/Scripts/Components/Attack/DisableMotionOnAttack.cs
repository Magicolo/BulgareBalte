using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Attack")]
public class DisableMotionOnAttack : ComponentBase
{

	void OnStartAttacking()
	{
		var motions = Entity.GetComponents<MotionBase>();
		foreach (var motion in motions)
		{
			motion.Active = false;
		}

	}

	void OnStopAttacking()
	{
		var motions = Entity.GetComponents<MotionBase>();
		foreach (var motion in motions)
		{
			motion.Active = true;
		}

	}
}

