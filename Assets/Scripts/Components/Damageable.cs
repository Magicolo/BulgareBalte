using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Damageable : PMonoBehaviour, IDamageable
{
	public float Health = 10f;
	public float CurrentHealth { get; set; }

	public virtual bool CanBeDamagedBy(DamageData damage)
	{
		return true;
	}

	public virtual void Damage(DamageData damage)
	{
		if (CanBeDamagedBy(damage))
			CurrentHealth -= damage.Damage;
	}
}
