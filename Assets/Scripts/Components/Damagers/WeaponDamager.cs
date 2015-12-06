using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class WeaponDamager : ModifierDamager
{
	public override void SetDamageData(DamageData damage)
	{
		base.SetDamageData(damage);

		this.damage.Types |= DamageTypes;
	}
}
