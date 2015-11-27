using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class WeaponBase : PComponent
{
	public float DamageModifier = 1f;
	public float AttackSpeedModifier = 1f;
	public DamageTypes DamageType;

	protected DamageData damage;

	public virtual void Attack(DamageData damage)
	{
		damage.Damage *= DamageModifier;
		damage.Type = DamageType;

		TypePoolManager.Recycle(damage);
		this.damage = damage;
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		TypePoolManager.Recycle(damage);
	}
}
