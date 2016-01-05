using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Damageable : ComponentBase
{
	public EntityGroupDefinition BySources;
	[EnumFlags]
	public DamageTypes ByTypes = (DamageTypes)int.MaxValue;

	public virtual bool CanBeDamagedBy(DamageData damage)
	{
		return ((BySources.Groups & ~damage.Sources.Groups) != BySources.Groups) && ((ByTypes & ~damage.Types) != ByTypes);
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
