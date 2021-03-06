﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent), typeof(DamagerBase))]
public class WeaponAttack : AttackBase, IStartable
{
	[EntityRequires(typeof(DamagerBase), typeof(AttackBase))]
	public PEntity StartWeapon;
	public Transform WeaponRoot;
	public float AttackSpeed = 1f;

	protected PEntity weapon;
	public float lastAttackTime { get; protected set; }

	public virtual void Start()
	{
		EquipWeapon(StartWeapon);
	}

	public void EquipWeapon(PEntity weaponPrefab)
	{
		UnequipWeapon();

		if (weaponPrefab == null)
			return;

		weapon = PrefabPoolManager.Create(weaponPrefab);
		weapon.Transform.parent = WeaponRoot;
		weapon.Transform.localPosition = Vector3.zero;
		weapon.Transform.localRotation = Quaternion.identity;

		var time = Entity.GetComponent<TimeComponent>();
		TimeComponent weaponTime;

		if (weapon.TryGetComponent(out weaponTime))
			weaponTime.Channel = time.Channel;

		weapon.SendMessage(EntityMessages.OnEquip);
		Entity.SendMessage(EntityMessages.OnEquip, weapon);
	}

	public void UnequipWeapon()
	{
		if (weapon != null)
		{
			weapon.SendMessage(EntityMessages.OnUnequip);
			Entity.SendMessage(EntityMessages.OnUnequip, weapon);
			PrefabPoolManager.Recycle(ref weapon);
		}
	}

	public float GetAttackSpeed()
	{
		float attackSpeed = AttackSpeed;

		if (weapon != null)
		{
			var modifiers = weapon.GetComponents<AttackSpeedModifier>();

			for (int i = 0; i < modifiers.Count; i++)
				attackSpeed *= modifiers[i].Modifier;
		}

		return attackSpeed;
	}

	public override void Attack()
	{
		if (weapon == null)
			return;

		var time = Entity.GetComponent<TimeComponent>();
		var damager = Entity.GetComponent<DamagerBase>();

		weapon.GetComponent<DamagerBase>().SetDamageData(damager.GetDamageData());
		weapon.GetComponent<AttackBase>().Attack();
		lastAttackTime = time.Time;
	}

	protected void OnDie()
	{
		UnequipWeapon();
	}
}
