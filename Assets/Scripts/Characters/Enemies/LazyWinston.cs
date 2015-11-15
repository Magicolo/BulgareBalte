using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(Rigidbody2D))]
public class LazyWinston : SeekerEnemy
{
	public float FireDuration = 1f;
	public WeaponBase StartWeapon;

	float nextAttackTime;
	float stopAttackTime;

	protected override void Update()
	{
		base.Update();
		UpdateWeapon();
	}

	void UpdateWeapon()
	{
		if (currentEquipment.Weapon == null)
			return;

		if (CachedTime.Time < stopAttackTime)
			currentEquipment.Weapon.Fire();
		else if (CachedTime.Time > nextAttackTime)
		{
			stopAttackTime = CachedTime.Time + FireDuration;
			nextAttackTime = stopAttackTime + 1f / currentStats.AttackSpeed;
		}
	}

	public override void OnCreate()
	{
		base.OnCreate();

		if (StartWeapon != null)
			EquipWeapon(StartWeapon);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		UnequipWeapon();
	}
}
