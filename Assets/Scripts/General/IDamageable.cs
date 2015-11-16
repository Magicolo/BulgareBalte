using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public enum DamageSources
{
	None,
	Player,
	Enemy
}

public enum DamageTypes
{
	None,
	Laser,
	Plasma,
	Fire
}

public interface IDamageable
{
	bool CanBeDamagedBy(DamageData damage);
	void Damage(DamageData damage);
}
