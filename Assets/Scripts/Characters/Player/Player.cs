using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pseudo;

public class Player : CharacterBase
{
	public static readonly List<Player> Players = new List<Player>();

	public InputManager.Players Input;
	public WeaponBase StartWeapon;

	public bool IsFiring { get; private set; }

	float nextAttackTime;

	public static Player GetClosest(Vector3 position)
	{
		Player closest = null;
		float closestDistance = float.MaxValue;

		for (int i = 0; i < Players.Count; i++)
		{
			Player player = Players[i];
			float distance = (player.Transform.position - position).sqrMagnitude;

			if (distance < closestDistance)
			{
				closest = player;
				closestDistance = distance;
			}
		}

		return closest;
	}

	void Awake()
	{
		OnCreate();
	}

	void Update()
	{
		UpdateInput();
		UpdateWeapon();
	}

	void UpdateInput()
	{
		IsFiring = InputManager.Instance.GetKey(Input, "Fire");
	}

	void UpdateWeapon()
	{
		if (IsFiring && CachedTime.Time > nextAttackTime && currentEquipment.Weapon != null)
		{
			currentEquipment.Weapon.Fire(new DamageData(currentStats.Damage, currentStats.DamageSource));
			nextAttackTime = CachedTime.Time + 1f / (currentStats.AttackSpeed * currentEquipment.Weapon.AttackSpeedModifier);
		}
	}

	public override void OnCreate()
	{
		base.OnCreate();

		Players.Add(this);

		if (StartWeapon != null)
			EquipWeapon(StartWeapon);
	}

	public override void OnRecycle()
	{
		base.OnRecycle();

		Players.Remove(this);
		UnequipWeapon();
	}

	public override void Kill()
	{

	}

	public override bool CanBeDamagedBy(DamageData damage)
	{
		return true;
	}
}
