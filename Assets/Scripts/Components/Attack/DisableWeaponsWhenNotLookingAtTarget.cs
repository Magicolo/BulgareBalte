using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Attack"), RequireComponent(typeof(SeekerMotion))]
public class DisableWeaponsWhenNotLookingAtTarget : ComponentBase, IUpdateable
{
	public bool ResetVelocityWhenAttacking;

	public float UpdateRate { get { return 0; } }

	[EntityRequires(typeof(WeaponAttack))]
	public PEntity Weapon;

	enum State { Reset, Targetting, Firing };
	State currentState;

	public Animator Animator;
	public string ReactivateAnimationName;
	public string ReactivateTriggerName;

	public void Update()
	{
		var seekerMotion = Entity.GetComponent<SeekerMotion>();

		switch (currentState)
		{
			case State.Reset:
				currentState = State.Targetting;
				Weapon.GetComponent<WeaponAttack>().Active = false;
				break;

			case State.Targetting:
				if (seekerMotion.LookingAtTarget)
				{
					currentState = State.Firing;
					if (ResetVelocityWhenAttacking)
						Entity.GameObject.GetComponent<Rigidbody2D>().SetVelocity(0);

					if (Animator != null)
						Animator.SetTrigger(ReactivateTriggerName);
					else
						Weapon.GetComponent<WeaponAttack>().Active = true;
				}
				break;
			case State.Firing:
				break;
		}
	}

	public void OnStopAttacking()
	{
		currentState = State.Reset;
	}

	void OnStateExit(AnimatorStateInfo info)
	{
		Weapon.GetComponent<WeaponAttack>().Active = true;
	}
}

