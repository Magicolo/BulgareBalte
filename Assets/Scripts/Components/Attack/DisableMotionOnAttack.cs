using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Attack")]
public class DisableMotionOnAttack : ComponentBase
{
	public bool ResetVelocityOnAttack;
	public Animator Animator;
	public string AnimationTriggerOnActivate;
	public string AnimationNameOnActivate;

	public float UpdateRate { get { return 0f; } }

	void OnStartAttacking()
	{
		var motions = Entity.GetComponents<MotionBase>();
		foreach (var motion in motions)
		{
			motion.Active = false;
		}

		if (ResetVelocityOnAttack)
			Entity.GameObject.GetComponent<Rigidbody2D>().SetVelocity(0);
	}

	void OnStopAttacking()
	{
		if (Animator != null && !string.IsNullOrEmpty(AnimationTriggerOnActivate))
			Animator.SetTrigger(AnimationTriggerOnActivate);
		else
			ActivateAllMotions();

	}

	void OnStateExit(AnimatorStateInfo info)
	{
		if (info.IsName(AnimationNameOnActivate))
			ActivateAllMotions();
	}



	void ActivateAllMotions()
	{
		var motions = Entity.GetComponents<MotionBase>();
		foreach (var motion in motions)
		{
			motion.Active = true;
		}
	}
}

