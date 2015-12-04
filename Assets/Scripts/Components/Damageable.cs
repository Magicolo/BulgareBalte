using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Damageable : PComponent
{
	public virtual bool CanBeDamagedBy(DamageData damage)
	{
		return true;
	}

	public virtual void Damage(DamageData damage)
	{
		if (CanBeDamagedBy(damage))
			Entity.SendMessage("OnDamaged", damage);
	}
}
