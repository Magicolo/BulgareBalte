using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Animator")]
public class AnimateOnStart : ComponentBase, IStartable
{
	public Animator Animator;
	public string TriggerName;

	[Space()]
	public bool DisactivateMotionWhileAnimating;
	public bool DisactivateAttacksWhileAnimating;
	public bool DisactivateDamagersWhileAnimating;
	public string AnimationName;

	public void Start()
	{
		Animator.SetTrigger(TriggerName);

		if (DisactivateDamagersWhileAnimating)
			foreach (var item in Entity.GetComponents<DamagerBase>())
				item.Active = false;

		if (DisactivateAttacksWhileAnimating)
			foreach (var item in Entity.GetComponents<AttackBase>())
				item.Active = false;

		if (DisactivateMotionWhileAnimating)
			foreach (var item in Entity.GetComponents<MotionBase>())
				item.Active = false;

	}

	void OnStateExit(AnimatorStateInfo info)
	{

		if (info.IsName(AnimationName))
		{
			if (DisactivateDamagersWhileAnimating)
				foreach (var item in Entity.GetComponents<DamagerBase>())
					item.Active = true;

			if (DisactivateMotionWhileAnimating)
				foreach (var item in Entity.GetComponents<MotionBase>())
					item.Active = true;
		}
	}
}

