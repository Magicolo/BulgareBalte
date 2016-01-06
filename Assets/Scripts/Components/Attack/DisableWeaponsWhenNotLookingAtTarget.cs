using UnityEngine;
using System.Collections.Generic;
using Pseudo;
using System;

[Serializable, ComponentCategory("Attack"), RequireComponent(typeof(SeekerMotion))]
public class DisableWeaponsWhenNotLookingAtTarget : ComponentBase, IUpdateable
{
	public float UpdateRate { get { return 0; } }

	public bool CanDisableWhenFiring = false;
	[EntityRequires(typeof(WeaponAttack))]
	public PEntity Weapon;

	public void Update()
	{
		var seekerMotion = Entity.GetComponent<SeekerMotion>();
		PDebug.Log(seekerMotion.LookingAtTarget, justAttacked());
		if ((CanDisableWhenFiring && justAttacked()) || !justAttacked())
			Weapon.GetComponent<WeaponAttack>().Active = seekerMotion.LookingAtTarget;
	}

	bool justAttacked()
	{
		var time = Entity.GetComponent<TimeComponent>();
		return time.Time - Weapon.GetComponent<WeaponAttack>().lastAttackTime < 0.5f;

	}
}

