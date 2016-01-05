using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable]
public class Damageable : ComponentBase
{
	[EntityGroups]
	public ByteFlag BySources;
	[EnumFlags]
	public DamageTypes ByTypes = (DamageTypes)int.MaxValue;

	public virtual bool CanBeDamagedBy(DamageData damage)
	{
		return ((BySources & ~damage.Sources) != BySources) && ((ByTypes & ~damage.Types) != ByTypes);
	}

	public virtual bool Damage(DamageData damage)
	{
		if (CanBeDamagedBy(damage))
		{
			Entity.SendMessage(EntityMessages.OnDamaged, damage);
			return true;
		}
		else
			return false;
	}
}
