using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class SourceDamager : DamagerBase
{
	public float DamageAmount = 1f;
	public DamageSources DamageSource;

	public override DamageData GetDamageData()
	{
		return new DamageData(DamageAmount, DamageSource);
	}

	public override void SetDamageData(DamageData damage)
	{
		DamageAmount = damage.Damage;
		DamageSource = damage.Source;
	}
}
