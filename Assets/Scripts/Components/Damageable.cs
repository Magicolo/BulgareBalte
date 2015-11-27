using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Damageable : PComponent, IDamageable
{
	public virtual bool CanBeDamagedBy(DamageData damage)
	{
		return true;
	}

	public virtual void Damage(DamageData damage)
	{
		if (CanBeDamagedBy(damage))
			SendMessage("OnDamaged", damage, SendMessageOptions.DontRequireReceiver);
	}
}
