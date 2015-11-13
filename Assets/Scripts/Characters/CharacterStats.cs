using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

[Serializable, Copy]
public class CharacterStats : IPoolable, ICopyable<CharacterStats>
{
	public static readonly Pool<CharacterStats> Pool = new Pool<CharacterStats>(() => new CharacterStats());

	public float Health = 10f;
	public float MovementSpeed = 50f;
	public float AimSpeed = 3f;
	public float AttackSpeed = 1f;
	public float Damage;
	public DamageSources DamageSource;

	public void OnCreate() { }
	public void OnRecycle() { }

	public void Copy(CharacterStats reference)
	{
		Health = reference.Health;
		MovementSpeed = reference.MovementSpeed;
		AimSpeed = reference.AimSpeed;
		AttackSpeed = reference.AttackSpeed;
		Damage = reference.Damage;
		DamageSource = reference.DamageSource;
	}
}
