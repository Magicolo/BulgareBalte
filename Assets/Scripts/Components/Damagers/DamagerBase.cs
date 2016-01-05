using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[ComponentCategory("Attack")]
public abstract class DamagerBase : ComponentBase
{
	public virtual void Damage(Damageable damageable)
	{
		if (damageable == null)
			return;

		if (damageable.Damage(GetDamageData()))
			Entity.SendMessage(EntityMessages.OnDamage, damageable);
	}

	public abstract DamageData GetDamageData();
	public abstract void SetDamageData(DamageData damage);
}
