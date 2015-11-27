using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[RequireComponent(typeof(TimeComponent))]
public abstract class AttackBase : PComponent
{
	public WeaponBase Weapon;
	public float AttackSpeed = 1f;
	public float Damage = 1f;
	public DamageSources DamageSource;

	protected float lastAttackTime;

	readonly CachedValue<TimeComponent> cachedTime;
	public TimeComponent CachedTime { get { return cachedTime; } }

	protected AttackBase()
	{
		cachedTime = new CachedValue<TimeComponent>(GetComponent<TimeComponent>);
	}

	protected virtual void Update()
	{
		UpdateAttack();
	}

	protected virtual void UpdateAttack()
	{
		if (Weapon != null && ShouldAttack())
			Attack();
	}

	protected virtual void Attack()
	{
		var damage = TypePoolManager.Create<DamageData>();
		damage.Initialize(Damage, DamageSource);
		Weapon.Attack(damage);
		lastAttackTime = CachedTime.Time;
	}

	protected abstract bool ShouldAttack();
}
