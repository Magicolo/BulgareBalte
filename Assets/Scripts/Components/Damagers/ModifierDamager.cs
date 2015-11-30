using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class ModifierDamager : DamagerBase
{
	public float DamageModifier = 1f;

	protected DamageData damage;

	public override DamageData GetDamageData()
	{
		return damage;
	}

	public override void SetDamageData(DamageData damage)
	{
		damage.Damage *= DamageModifier;
		this.damage = damage;
	}
}
