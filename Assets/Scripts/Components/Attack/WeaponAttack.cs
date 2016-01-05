using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, EntityRequires(typeof(TimeComponent))]
public class WeaponAttack : AttackBase
{
	[EntityRequires(typeof(DamagerBase), typeof(AttackBase))]
	public PEntity StartWeapon;
	public Transform WeaponRoot;
	public float AttackSpeed = 1f;

	protected PEntity weapon;
	protected float lastAttackTime;

	public void EquipWeapon(PEntity weaponPrefab)
	{
		UnequipWeapon();

		if (weaponPrefab == null)
			return;

		weapon = PrefabPoolManager.Create(weaponPrefab);
		weapon.CachedTransform.parent = WeaponRoot;
		weapon.CachedTransform.localPosition = Vector3.zero;
		weapon.CachedTransform.localRotation = Quaternion.identity;

		var time = Entity.GetComponent<TimeComponent>();
		TimeComponent weaponTime;

		if (weapon.TryGetComponent(out weaponTime))
			weaponTime.Channel = time.Channel;
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
		var time = Entity.GetComponent<TimeComponent>();

		weapon.GetComponent<DamagerBase>().SetDamageData(Damager.GetDamageData());
		weapon.GetComponent<AttackBase>().Attack();
		lastAttackTime = time.Time;
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
