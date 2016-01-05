using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class ModifierDamager : DamagerBase
{
	public float DamageModifier = 1f;
	[EnumFlags]
	public DamageTypes DamageTypes;

	protected DamageData damage;

	public override DamageData GetDamageData()
	{
		return damage;
	}

	public override void SetDamageData(DamageData damage)
	{
		damage.Damage *= DamageModifier;
		damage.Sources |= Entity.Groups;
		damage.Types |= DamageTypes;
		this.damage = damage;
	}
}
