﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Bully : Enemy
{
	public float FireDuration = 1f;
	public WeaponBase StartWeapon;

	float nextAttackTime;

	protected virtual void Update()
	{
		UpdateWeapon();
	}

	void UpdateWeapon()
	{
		if (currentEquipment.Weapon == null)
			return;

		if (CachedTime.Time > nextAttackTime)
		{
			currentEquipment.Weapon.Fire(new DamageData(CurrentStats.Damage, CurrentStats.DamageSource));
			nextAttackTime = CachedTime.Time + 1f / currentStats.AttackSpeed;
		}
	}

	public override void OnCreate()
	{
		base.OnCreate();

		EquipWeapon(StartWeapon);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		UnequipWeapon();
	}
}