using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public class WeaponAttack : AttackBase
{
	[Requires(typeof(DamagerBase), typeof(AttackBase))]
	public PEntity StartWeapon;
	public Transform WeaponRoot;
	public float AttackSpeed = 1f;

	protected PEntity weapon;
	protected float lastAttackTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	protected WeaponAttack()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	public void EquipWeapon(PEntity weaponPrefab)
	{
		UnequipWeapon();

		if (weaponPrefab == null)
			return;

		weapon = PrefabPoolManager.Create(weaponPrefab);
		weapon.CachedTransform.parent = WeaponRoot;
		weapon.CachedTransform.localPosition = Vector3.zero;
		weapon.CachedTransform.localRotation = Quaternion.identity;

		TimeComponent time;
		if (weapon.TryGetComponent(out time))
			time.Channel = CachedTime.Channel;
	}

	public void UnequipWeapon()
	{
		PrefabPoolManager.Recycle(ref weapon);
	}

	public float GetAttackSpeed()
	{
		float attackSpeed = AttackSpeed;

		if (weapon != null)
		{
			var modifiers = weapon.GetComponents<AttackSpeedModifier>();
			for (int i = 0; i < modifiers.Count; i++)
			{
				attackSpeed *= modifiers[i].Modifier;
			}
		}

		return attackSpeed;
	}

	public override void Attack()
	{
		weapon.GetComponent<DamagerBase>().SetDamageData(Damager.GetDamageData());
		weapon.GetComponent<AttackBase>().Attack();
		lastAttackTime = CachedTime.Time;
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
