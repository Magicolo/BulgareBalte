using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class WeaponBase : PMonoBehaviour
{
	public float DamageModifier = 1f;
	public float AttackSpeedModifier = 1f;
	public DamageTypes DamageType;

	public abstract void Fire(DamageData damage);
}
