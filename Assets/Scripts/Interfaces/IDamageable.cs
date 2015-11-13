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
	Bullet,
	Explosion
}

public interface IDamageable
{
	bool CanBeDamagedBy(DamageSources damageSource, DamageTypes damageType);
	void Damage(DamageData data);
}
