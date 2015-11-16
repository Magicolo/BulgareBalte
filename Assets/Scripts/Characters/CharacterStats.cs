using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, Copy]
public class CharacterStats : ICopyable<CharacterStats>
{
	public float Health = 10f;
	public float AttackSpeed = 1f;
	public float Damage;
	public DamageSources DamageSource;

	public void Copy(CharacterStats reference)
	{
		Health = reference.Health;
		AttackSpeed = reference.AttackSpeed;
		Damage = reference.Damage;
		DamageSource = reference.DamageSource;
	}
}
