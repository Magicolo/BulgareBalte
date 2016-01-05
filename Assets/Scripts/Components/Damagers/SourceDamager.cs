using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class SourceDamager : DamagerBase
{
	public float DamageAmount = 1f;

	public override DamageData GetDamageData()
	{
		return new DamageData(DamageAmount, Entity.Groups);
	}

	public override void SetDamageData(DamageData damage)
	{
		DamageAmount = damage.Damage;
	}
}
