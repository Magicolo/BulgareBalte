using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class DamagerBase : PComponent
{
	public virtual void Damage(IDamageable damageable)
	{
		if (damageable == null)
			return;

		var damage = GetDamageData();
		if (damageable.CanBeDamagedBy(damage))
		{
			damageable.Damage(damage);
			SendMessage("OnDamage", damageable, SendMessageOptions.DontRequireReceiver);
		}
	}

	public abstract DamageData GetDamageData();
	public abstract void SetDamageData(DamageData damage);
}
