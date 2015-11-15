using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public abstract class WeaponBase : PMonoBehaviour
{
	public DamageTypes DamageType;
	[Min]
	public float AttackSpeedModifier = 3f;
	public float DamageModifier = 1f;

	public CharacterBase Owner { get; set; }

	public abstract void Fire();
}
